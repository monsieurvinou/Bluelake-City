using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {
	static DialogBox _instance;
	public GameObject background;
	public GameObject text;

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

	public static void CreateDialog(GameObject target, string content, float width, float height, float offsetX=0, float offsetY=0) {
		instance.gameObject.SetActive (true);
		Text textComponent = instance.text.GetComponent<Text> ();
		RectTransform textRect = instance.text.GetComponent<RectTransform>();
		RectTransform backgroundRect = instance.background.GetComponent<RectTransform> ();
		textComponent.text = content;

		backgroundRect.sizeDelta = new Vector2 (width, height);
		textRect.sizeDelta = new Vector2 (width - 10, height - 10);

		RectTransform dialogRect = instance.gameObject.GetComponent<RectTransform> ();
		Vector3 newPos = GameObject.Find ("Camera").camera.WorldToScreenPoint(target.transform.position);
		newPos.x += offsetX;
		newPos.y += offsetY;
		dialogRect.position = newPos;

		// Define for the update
		instance.currentTarget = target;
		instance.currentOffsetX = offsetX;
		instance.currentOffsetY = offsetY;
	}

	void Update() {
		if (gameObject.activeInHierarchy) {
			if ( currentTarget != null ) {
				RectTransform dialogRect = instance.gameObject.GetComponent<RectTransform> ();
				Vector3 newPos = GameObject.Find ("Camera").camera.WorldToScreenPoint(currentTarget.transform.position);
				newPos.x += currentOffsetX;
				newPos.y += currentOffsetY;
				dialogRect.position = newPos;
			}
		}
	}
}
