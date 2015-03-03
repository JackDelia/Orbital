using UnityEngine;
using System.Collections;

//govern the behaviour of the ship
public class ShipController : MonoBehaviour {

	//The ship's initial velocity
	public float speedX;
	public float speedY;
	
	//The power of the ship's first thrust
	public float initPower;
	public float power;

	//The angle of the ship's rotation in radians
	public float theta;

	//whether the first thrust has been applied
	public bool active;
	//used to lock the ship to a planet if the starting planet is moving.
	public GameObject locked;
	public Vector3 relativePos;

	//Number of times it can still boost
	public int boosts;
	//An array of the sprites for the boosts, used to remove them from the screen once used
	public GameObject[] boostImages;

	//Used to track the power and angle of the initial thrust
	public Vector3 startPos;
	public Vector3 pos;

	//Sets the inial velocity.
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (speedX, speedY);	

		if (locked) {
			relativePos = new Vector3(transform.position.x-locked.transform.position.x, 
			                          transform.position.y-locked.transform.position.y,
			                          transform.position.z-locked.transform.position.z);
				}
	}

	//detects collisions
	//if it's the goal, proceed to the next level, otherwise restart the level
	public void OnCollisionEnter2D(Collision2D collision){
		Planetary planet = collision.gameObject.GetComponent<Planetary> ();
		if (planet.goal) {
			Debug.Log ("VICTORY");
			Application.LoadLevel (Application.loadedLevel + 1);
				} 
		else {
			if (active || planet.iminent) {
				Debug.Log ("FAILURE");
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		//if the ship has already launched, the ship faces prograde
		if(active)
			theta = Mathf.Atan ((gameObject.rigidbody2D.velocity.y / gameObject.rigidbody2D.velocity.x));
		//k is theta in degrees
		float k = theta * (57.3f) - 90;

		//stops theta from being messed up by the fact that arctangents are not functional
		if (gameObject.rigidbody2D.velocity.x >= 0)
			transform.rotation = Quaternion.Euler (0, 0, k);
		else
			transform.rotation = Quaternion.Euler (0, 0, k + 180);
		if(!active && startPos.x > pos.x)
			transform.rotation = Quaternion.Euler (0, 0, k + 180);

		//reads in the boost commands when it has already launched
		if (active) {
			if (Input.GetButtonDown ("Fire1") && boosts >0) {
				boosts--;
				Destroy(boostImages[boosts]);
				float dx = Mathf.Cos (theta) * (power/10);
				float dy = Mathf.Sin (theta) * (power/10);
				Vector2 boost;
								
				if (gameObject.rigidbody2D.velocity.x > 0)
					boost = new Vector2 (dx, dy);
				else
					boost = new Vector2 (-dx, -dy);
	
				gameObject.rigidbody2D.AddForce (boost);
			}

			//resets the level
			if(Input.GetButtonDown("Fire2"))
			   Application.LoadLevel(Application.loadedLevelName);
		} 

		//used to aim the ship for the initial launch
		else {
			if(locked){
				transform.position = locked.transform.position+relativePos;
			}

			if(Input.GetMouseButtonDown(0)){
				startPos = Input.mousePosition;
			}
		
			if (Input.GetMouseButton(0)){
				pos = Input.mousePosition;
				float dx = pos.x - (startPos.x);
				float dy = pos.y - (startPos.y);

				initPower = Mathf.Sqrt(Mathf.Pow(dx,2)+Mathf.Pow(dy,2));
				if(initPower > 100)
					initPower = 100;
				if(dx!=0)
					theta = Mathf.Atan(dy/dx);

		}
				
			//launches the ship upon release
			if(Input.GetMouseButtonUp(0)){
				if(startPos.x < pos.x)
					gameObject.rigidbody2D.AddForce (new Vector2(Mathf.Cos(theta)*power, Mathf.Sin(theta)*power));
				else
					gameObject.rigidbody2D.AddForce (new Vector2(-(Mathf.Cos(theta)*power), -(Mathf.Sin(theta)*power)));
				active = true;
				}
			}
	}
}
