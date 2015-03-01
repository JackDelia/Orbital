using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject following;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (following) {
			Vector3 angle = following.transform.position;
			angle.z = -10;
			transform.position = angle;
				}
	

	}
}
