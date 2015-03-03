using UnityEngine;
using System.Collections;


//used to govern the behaviour of planetary objects

public class Planetary : MonoBehaviour {

	//array of objects that are affected by this object's gravity
	public GameObject[] affected;

	//The graviational mass of the object.
	//Noted to be different than inertial mass.
	public float mass;

	//The initial speed in the X and Y directions of the object.
	public float speedX;
	public float speedY;

	//whether or not this object is the goal of the level.
	public bool goal;

	//whether it will destroy the ship on the launch pad
	public bool iminent;

	//Sets the inial velocity.
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (speedX, speedY);
	}
	
	// Update is called once per frame
	void Update () {
		//stop planets from spinning around
		transform.rotation = Quaternion.Euler (0, 0, 0);

		//for each object it affects with gravity, a force is applied in accordance with Newton's law of universal gravitation
		//The ship has a gravitational mass of 1
		foreach (GameObject g in affected) {
			if(!(g.GetComponent<ShipController>() && !(g.GetComponent<ShipController>().active))){
						Vector2 gravField;
						float dx = transform.position.x - g.transform.position.x;
						float dy = transform.position.y - g.transform.position.y;
						float hyp = Mathf.Sqrt (Mathf.Pow (dx, 2) + Mathf.Pow (dy, 2));
						float totForce = mass / (.5f*(Mathf.Pow (dx, 2) + Mathf.Pow (dy, 2)));
						gravField.x = (dx / hyp) * totForce;
						gravField.y = (dy / hyp) * totForce;
						g.rigidbody2D.AddForce (gravField);
			}
				}
	}
}
