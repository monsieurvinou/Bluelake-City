using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public static bool isAllowed = true;
	public bool isVisible = false;
	public List<ItemButton> listButtons;

	Vector3 hiddenPosition;
	Vector3 shownPosition;

	GameObject description;
	Text descriptionText;

	void Start() {
		RectTransform rect = GetComponent<RectTransform>();

		hiddenPosition = rect.position;
		shownPosition = rect.position;
		shownPosition.y -= rect.rect.size.y;

		description = GameObject.Find ("Description");
		description.GetComponent<Image> ().CrossFadeAlpha (0f, 0f, true);
		descriptionText = GameObject.Find ("Description_Text").GetComponent<Text> ();

		ItemButton.OnSelected += SetDescription;
		
		listButtons = new List<ItemButton>();
		int i = 0;
		for ( i = 0; i<6; i++ ) {
			listButtons.Add(GameObject.Find("Item_" + i).GetComponent<ItemButton>());
			Item it = Inventory.instance.GetItem(i);
			if ( it != null ) {
				listButtons[i].item = it;
			}
		}
	}

	void SetDescription(ItemButton item) {
		if ( item != null && item.item != null ) descriptionText.text = item.item.description;
		else descriptionText.text = "";
	}

	public void ToggleMenu() {
		ItemButton.Refresh();
		Vector3 targetPosition;
		if ( isVisible ) {
			targetPosition = hiddenPosition;
			PlayerMovement.allowInput = true;
			Conversation.isAllowed = true;
			description.GetComponent<Image>().CrossFadeAlpha(0f, 0.2f,false);
			descriptionText.CrossFadeAlpha(0f, 0.2f, false);
		} else {
			targetPosition = shownPosition;
			EventSystem.current.SetSelectedGameObject( GameObject.Find("Item_0") );
			PlayerMovement.allowInput = false;
			Conversation.isAllowed = false;
			description.GetComponent<Image>().CrossFadeAlpha(1f, 0.2f,false);
			descriptionText.CrossFadeAlpha(1f, 0.2f, false);
		}

		iTween.MoveTo(gameObject, targetPosition, 0.2f);
		isVisible = !isVisible;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory") && InventoryManager.isAllowed ) {
			ToggleMenu ();
		}
	}
}
