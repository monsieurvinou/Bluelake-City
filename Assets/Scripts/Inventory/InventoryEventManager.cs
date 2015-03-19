using UnityEngine;
using System.Collections;

public class InventoryEventManager : MonoBehaviour {
	public delegate void ItemAction(ItemButton item);
	public static event ItemAction ItemDispatch;

	public static void Dispatch(ItemButton item) {
		if (ItemDispatch != null) 
			ItemDispatch (item);
	}
}