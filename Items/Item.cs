using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
	public Sprite icon = null;
	public bool isDefaultItem = false;
	
	public virtual void Use ()
	{
		// We want to override this to get different uses for each item
	}
	
	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
}
