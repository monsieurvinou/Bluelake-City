using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {
	static DialogBox _instance;
	public GameObject background;
	public GameObject text;
	public GameObject arrow;

	GameObject currentTarget;
	float currentOffsetX = 0;
	float currentOffsetY = 0;

	public static DialogBox instance {
		get {
			if ( _instance == null ) {
				_instance = GameObject.FindObjectOfType <DialogBox>();
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Awake () {
		instance.gameObject.SetActive (false); // Desactivate by default
	}

	public static void CloseDialog() {
		instance.gameObject.SetActive (false);
	}

	public static void CreateDialog(DialogMessage message) {
		instance.gameObject.SetActive (true);
		Text textComponent = instance.text.GetComponent<Text> ();
		RectTransform textRect = instance.text.GetComponent<RectTransform>();
		RectTransform backgroundRect = instance.background.GetComponent<RectTransform> ();
		textComponent.text = message.content;

		backgroundRect.sizeDelta = new Vector2 (message.width, message.height);
		textRect.sizeDelta = new Vector2 (message.width - 10, message.height - 10);

		RectTransform dialogRect = instance.gameObject.GetComponent<RectTransform> ();
		Vector3 newPos = GameObject.Find ("Camera").GetComponent<Camera>().WorldToScreenPoint(message.target.transform.position);
		newPos.x += message.offsetX;
		newPos.y += message.offsetY;
		dialogRect.position = newPos;

		// Define for the update
		instance.currentTarget = message.target;
		instance.currentOffsetX = message.offsetX;
		instance.currentOffsetY = message.offsetY;
	}

	void Update() {
		if (gameObject.activeInHierarchy) {
			if ( currentTarget != null ) {
				RectTransform dialogRect = instance.gameObject.GetComponent<RectTransform> ();
				Vector3 newPos = GameObject.Find ("Camera").GetComponent<Camera>().WorldToScreenPoint(currentTarget.transform.position);
				newPos.x += currentOffsetX;
				newPos.y += currentOffsetY;
				dialogRect.position = newPos;
			}
		}
	}
}
