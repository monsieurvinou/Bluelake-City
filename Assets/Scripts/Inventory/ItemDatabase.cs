using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ItemDatabase")]
public class ItemDatabase {
	public static string filePath = "Data/itemDatabase.xml";

	[XmlArray("Items"),XmlArrayItem("Item")]
	public List<Item> Items;
	//public Item[] Items;

	public Item GetItem(int id) {
		Item result = Items.Find(
			delegate ( Item it ) {
				return it.itemID == id;
			}
		);
		if ( result != null ) return result;
		else return new Item();
	}

	public void Add(Item item) {
		Items.Add(item);
	}

	public static ItemDatabase Load(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
		using(FileStream stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ItemDatabase;
		}
	}
	
}