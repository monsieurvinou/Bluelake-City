using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	Conversation conv;

	void Start() {
		conv = GetComponent<Conversation> ();
	}

	void OnTriggerStay(Collider other) {
		if ( other.tag == "Player") {
			if (Input.GetButtonDown ("Submit")) {
				conv.StartConversation();
			}
		}
	}
}
