using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerMovement : MonoBehaviour {
	private Animator animator;
	public float speed = 10f;
	public static bool allowInput = true;
	Quaternion cameraRotation;

	Vector3 movement;

	void Awake() {
		cameraRotation = GameObject.Find ("Camera").transform.rotation;
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (allowInput) {
			// Calculate how fast we should be moving
			float h = Input.GetAxis ("Horizontal");
			float v = Input.	GetAxis ("Vertical");
			Move (h, v);
			Turning (h, v);
			Animate();
		}
	}

	void Move(float h, float v) {
		movement.Set (h, 0f, v);
		movement = cameraRotation * movement;
		movement.y = 0f;
		movement = movement.normalized * speed * Time.deltaTime;
		GetComponent<Rigidbody>().MovePosition (transform.position + movement);
	}
	
	void Animate() {
		if ( movement.x != 0 && movement.z != 0 ) 
			animator.SetFloat("Speed", 1);
		else
			animator.SetFloat("Speed", 0);
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
