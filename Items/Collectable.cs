using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable  // This inherits everything from the interactable script
{
	public Item item;
	int count = 0;
	public int actionsNeeded = 3;
	public string itemNeeded = "Hatchet";
	
    public override void Interact()
	{
		base.Interact();
		Collect();
	}	
	
   	void Collect()
	{
		if (EquipmentManager.equippedTool.name == itemNeeded)
		{
			count = count + 1;
			if (count == actionsNeeded)
			{			
				Inventory.instance.Add(item);
				//MessagesUI.SetMessage("Got " + item.name);
				count = 0;
			}
		} else {
			MessagesUI.SetMessage("Equip a " + itemNeeded);
		}
	}
}
