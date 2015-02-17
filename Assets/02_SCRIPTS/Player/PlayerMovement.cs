using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 10f;
	Quaternion cameraRotation;

	Vector3 movement;

	void Awake() {
		cameraRotation = GameObject.Find ("Camera").transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Calculate how fast we should be moving
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Move (h, v);
		Turning (h, v);
		if (Input.GetButtonDown ("Submit")) {
			DialogBox.CreateDialog(GameObject.Find ("Robert"), "Coucou, je m'appelle Robert!", 300f, 80f,0f, 180f);
		}
	}

	void Move(float h, float v) {
		movement.Set (h, 0f, v);
		movement = cameraRotation * movement;
		movement.y = 0f;
		movement = movement.normalized * speed * Time.deltaTime;
		rigidbody.MovePosition (transform.position + movement);
	}

	void Turning(float h, float v) {
		if (h != 0 || v != 0) {
			transform.eulerAngles = new Vector3(
				transform.eulerAngles.x, 
				Mathf.Atan2(h, v) * Mathf.Rad2Deg, 
				transform.eulerAngles.z
			);
		}

	}
}
