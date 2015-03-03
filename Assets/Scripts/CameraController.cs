using UnityEngine;
using System.Collections;


// Controlls the camera to follow the object set to following
//necessary to avoid the camera spinning when following an object.
public class CameraController : MonoBehaviour {

	public GameObject following;
	
	// Update is called once per frame
	void Update () {

		//if it is following an object, the camera is set to its position
		if (following) {
			Vector3 angle = following.transform.position;
			angle.z = -10;
			transform.position = angle;
				}
	

	}
}
