using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
	bool isRotating = false;
	
	Vector3 offset;                     // The initial offset from the target.
	
	void Start ()
	{
		// Calculate the initial offset.
		offset = transform.position - target.position;
	}
	
	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;
		
		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);


		if ( !isRotating && Input.GetAxisRaw("Rotate") != 0 ) {
			isRotating = true;
			float rotationToDo = 90f;
			if ( Input.GetAxisRaw("Rotate") < 0 ) {
				rotationToDo *= -1;
			}

			GameObject world = GameObject.Find("World");
			Hashtable args = new Hashtable(){
				{"time", 0.7f},
				{"amount", new Vector3(0, rotationToDo, 0)},
				{"oncomplete", "AllowRotation"}, 
				{"oncompletetarget", gameObject}
			};
			iTween.RotateAdd(world, args);
		}
	}

	void AllowRotation() {
		isRotating = false;
	}
}
