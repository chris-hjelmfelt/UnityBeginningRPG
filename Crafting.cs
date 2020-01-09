using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
	#region Singleton // this section is some simplification of using something called singletons (see 8:30 of E04)
	public static Crafting instance;  
	
	void Awake ()  // Start of game
	{
		if  (instance != null)
		{
			print("More than once instance of Crafting found!");
			return;
		}
		
		instance = this;
	}
	#endregion
			
	public int space = 20;
    public List<Item> items = new List<Item>();
	
	public bool Add (Item item)
	{		
		if (items.Count >= space)
		{
			print("Don't have enough room.");
			return false;
		}
		items.Add(item);	
		
		return true;
	}
	
	public void Remove (Item item)
	{
		items.Remove(item);	
	}
}
