using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float speedX;
	public float speedY;

	public float initPower;
	public float power;
	public float theta;
	public bool active;

	public int boosts;
	public GameObject[] boostImages;

	public Vector3 startPos;

	public Vector3 pos;

	// Use this for initialization
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (speedX, speedY);	
	}

	public void OnCollisionEnter2D(Collision2D collision){
		Planetary planet = collision.gameObject.GetComponent<Planetary> ();
		if (planet.goal) {
			Debug.Log ("VICTORY");
			Application.LoadLevel (Application.loadedLevel + 1);
				} 
		else {
			if (active) {
				Debug.Log ("FAILURE");
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(active)
			theta = Mathf.Atan ((gameObject.rigidbody2D.velocity.y / gameObject.rigidbody2D.velocity.x));
		float k = theta * (57.3f) - 90;
		if (gameObject.rigidbody2D.velocity.x >= 0)
			transform.rotation = Quaternion.Euler (0, 0, k);
		else
			transform.rotation = Quaternion.Euler (0, 0, k + 180);
		if(!active && startPos.x > pos.x)
			transform.rotation = Quaternion.Euler (0, 0, k + 180);

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

			if(Input.GetButtonDown("Fire2"))
			   Application.LoadLevel(Application.loadedLevelName);
		} 
		else {
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
