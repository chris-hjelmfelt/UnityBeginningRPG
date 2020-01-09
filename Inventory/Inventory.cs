using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton // this section is some simplification of using something called singletons (see 8:30 of E04)
	public static Inventory instance;  
	
	void Awake ()  // Start of game
	{
		if  (instance != null)
		{
			print("More than once instance of Inventory found!");
			return;
		}
		
		instance = this;
	}
	#endregion
	
	public delegate void OnItemChanged();   // This allows other scripts to listen for changes to inventory items
	public OnItemChanged onItemChangedCallback;
	
	public int space = 20;
    public List<Item> items = new List<Item>();
	
	public bool Add (Item item)
	{
		if (!item.isDefaultItem)
		{
			if (items.Count >= space)
			{
				print("Don't have enough room.");
				return false;
			}
			items.Add(item);	

			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();		
			}
		}
		return true;
	}
	
	public void Remove (Item item)
	{
		items.Remove(item);
		
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();	
		}			
	}
}
