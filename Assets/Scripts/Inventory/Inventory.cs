using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Inventory {
	public static Inventory _instance;
	public List<Item> items;
	private ItemDatabase db;
	private List<int> listId;
	
	public static Inventory instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new Inventory();
			}
			return _instance;
		}
	}

	public Inventory() {
		items = new List<Item> ();
		listId = new List<int> ();
		db = ItemDatabase.Load( Path.Combine(Application.dataPath, ItemDatabase.filePath) );
		string temp = PlayerPrefs.GetString ("Inventory");
		string[] tempArray = temp.Split ("*".ToCharArray ());
		for ( int i=0; i<tempArray.Length && temp != "" && temp != null; i++ ) {
			listId.Add(System.Int32.Parse(tempArray[i]));
			items.Add( db.GetItem( listId[i] ));
		}
	}

	public Item GetItem(int index) {
		if ( index < items.Count && items[index].itemID > 0 ) return items[index];
		else return null;
	}

	public bool Add(Item item) {
		if (items.Count < 6) {
			// We can add an item
			items.Add(item);
			listId.Add(item.itemID);
			Save();
			return true;
		}
		return false;
	}

	public bool Add(int id) {
		Item item = db.GetItem(id);
		return Add(item);
	}

	public bool Delete(Item item) {
		if (items.Contains (item)) {
			items.Remove(item);
			for ( int i=0; i<listId.Count; i++ ) {
				if ( listId[i] == item.itemID ) {
					// We remove this one
					listId[i] = item.itemID;
					return true;
				}
			}
		}

		return false;
	}

	public void Save() {
		string stringIds = "";
		for ( int i=0; i < listId.Count; i++ ) {
			stringIds += listId[i].ToString();
			if ( i < listId.Count - 1 ) {
				stringIds += "*";
			}
		}

		PlayerPrefs.SetString ("Inventory", stringIds);
	}
}