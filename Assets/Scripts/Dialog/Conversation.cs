using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {
	public DialogMessage[] messages;
	public static bool isAllowed = false;
	private int _index = 0;

	public void StartConversation() {
		if (Conversation.isAllowed) {
			if (_index < messages.Length) {
				DialogBox.CreateDialog (messages [_index]);
				PlayerMovement.allowInput = false;
				InventoryManager.isAllowed = false;
				_index ++;
			} else {
				DialogBox.CloseDialog();
				_index = 0;
				PlayerMovement.allowInput = true;
				InventoryManager.isAllowed = true;
			}
		}
	}
}
