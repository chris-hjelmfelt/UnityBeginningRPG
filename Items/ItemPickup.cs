using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable  // This inherits everything from the interactable script
{
	public Item item;
	
    public override void Interact()
	{
		base.Interact();
		
		Pickup();
	}
	
	void Pickup ()
	{
		bool wasPickedUp = Inventory.instance.Add(item);
		if (wasPickedUp)
		{
			Destroy(gameObject);
		}
	}
}
