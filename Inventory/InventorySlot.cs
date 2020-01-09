using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;
    Item item;
	private static GameObject myPrefab;
    GameObject go;
	
	public void AddItem (Item newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}
	
	public void ClearSlot ()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}
	
	public void onRemoveButton()
	{
		myPrefab = ItemReferences.GetType().GetField(item.name) ; // this is called reflection - https://forum.unity.com/threads/access-variable-by-string-name.42487/
		print(myPrefab);
		if (myPrefab) {
			go = Instantiate (myPrefab, transform.position + transform.forward, transform.rotation) as GameObject;
		}
		Inventory.instance.Remove(item);
		
	}
	
	public void UseItem()
	{
		if (item != null)
		{
			item.Use();
		}
	}
}
