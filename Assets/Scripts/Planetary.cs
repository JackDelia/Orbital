using UnityEngine;
using System.Collections;

public class Planetary : MonoBehaviour {

	public GameObject[] affected;
	public float mass;
	public float speedX;
	public float speedY;

	public bool goal;

	// Use this for initialization
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (speedX, speedY);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject g in affected) {
			if(g.GetComponent<ShipController>() && !(g.GetComponent<ShipController>().active))
				break;
						Vector2 gravField;
						float dx = transform.position.x - g.transform.position.x;
						float dy = transform.position.y - g.transform.position.y;
						float hyp = Mathf.Sqrt (Mathf.Pow (dx, 2) + Mathf.Pow (dy, 2));
						float totForce = mass / (Mathf.Pow (dx, 2) + Mathf.Pow (dy, 2));
						gravField.x = (dx / hyp) * totForce;
						gravField.y = (dy / hyp) * totForce;
						g.rigidbody2D.AddForce (gravField);
				}
	}
}
