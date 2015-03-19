using UnityEngine;
using System.Collections;

public class ItemEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InventoryEventManager.ItemDispatch += UseItem;
	}
	
	void UseItem(ItemButton item) {
		if (item != null && item.item != null) {
			if ( item.item.itemName == "Trumpet") UseTrumpet();
		}
	}


	void UseTrumpet() {
		Debug.Log ("Pouet");
	}
}
