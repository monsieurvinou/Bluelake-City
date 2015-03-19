using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Item {
	public string itemName;
	public int itemID = 0;
	public string description;
	public string texture = "";

	public Item(string name, int id, string desc, string text) {
		itemName = name;
		itemID = id;
		description = desc;
		texture = text;
	}

	public Item() {
		itemName = "";
		itemID = 0;
		description = "";
	}
}

