using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class ItemButton : MonoBehaviour, ISubmitHandler, ISelectHandler {
	public Item item;

	// EVENT
	public delegate void ItemSelected(ItemButton item);
	public static event ItemSelected OnSelected;

	public static void Refresh() {
		GameObject[] listButtonsGO = GameObject.FindGameObjectsWithTag("ItemButton");
		for (int i=0; i<listButtonsGO.Length; i++) {
			ItemButton ibb = listButtonsGO[i].GetComponent<ItemButton>();
			if ( ibb.item != null && ibb.item.itemID > 0 ) {
				if ( ibb.item.texture != "" ) {
					Image icon = GameObject.Find("Icon_" + ibb.gameObject.name).GetComponent<Image>();
					if ( icon.sprite == null || icon.sprite.name != ibb.item.texture ) {
						// We change the Sprite
						Sprite newSprite =  Resources.Load<Sprite>(ibb.item.texture);
						if (newSprite){
							newSprite.name = ibb.item.texture;
							icon.sprite = newSprite;
							icon.color = new Color(1f,1f,1f,1f);
						} else {
							Debug.LogError("Sprite '" + ibb.item.texture + "' not found.");
						}
					}
				}
				
			}
		}
	}

	public void OnSelect(BaseEventData eventData) {
		if (OnSelected != null)
			OnSelected (this);
	}

	public void OnSubmit(BaseEventData eventData) {
		if (GameObject.Find ("Inventory").GetComponent<InventoryManager> ().isVisible) {
			Debug.Log ("Submit " + name);
			GameObject.Find ("Inventory").GetComponent<InventoryManager> ().ToggleMenu ();
			InventoryEventManager.Dispatch (this);
		}
	}


}