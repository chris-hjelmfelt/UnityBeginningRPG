using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
	public GameObject CraftUI;	
	public GameObject timerBar;
	public Item plank;
	public Item rod;
	Image timerSlider;	
	//bool coolingDown = true;
	//float waitTime = 30.0f;
	
    // Start is called before the first frame update
    void Start()
    {
		timerSlider = timerBar.transform.GetChild(0).GetComponent<Image>();
		timerSlider.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
		{
			CraftUI.SetActive(!CraftUI.activeSelf);  // Open and Close the inventory menu
		}
		timerSlider.fillAmount = TimerBarHelper.fill;
    }
	
		
	// If player has a log and a knife craft a plank
	public void CraftPlank()
	{
		if (EquipmentManager.equippedTool.name == "Knife")
		{
			for (int i = 0; i < InventoryUI.inventory.items.Count; i++)
			{			
				if(InventoryUI.inventory.items[i].name == "Log")
				{
					StartCoroutine(CraftSequence(InventoryUI.inventory.items[i], plank, 5.0f));
					break;
				}
				
			}
		} else {
			print("Equip a knife");
		}
	}
	
	
	// If player has a log and a knife craft a wooden rod
	public void CraftRod()
	{
		if (EquipmentManager.equippedTool.name == "Knife")
		{
			for (int i = 0; i < InventoryUI.inventory.items.Count; i++)
			{			
				if(InventoryUI.inventory.items[i].name == "Log")
				{
					StartCoroutine(CraftSequence(InventoryUI.inventory.items[i], rod, 5.0f));
					return;
				}				
			}
		} else {
			print("Equip a knife");
		}
	}
	
	IEnumerator CraftSequence(Item toRemove, Item toAdd, float myTime)  // this is a coroutine - a sequence that prevents certain items from happening until others have finished
	{
		Inventory.instance.Remove(toRemove);
		MessagesUI.SetMessage("Crafting " + toAdd.name);
		yield return waitForTimer();
		yield return StartTimedAction();
		yield return TimedAction(myTime);
		Inventory.instance.Add(toAdd);	
	}
	
	IEnumerator waitForTimer()
	{
		while (TimerBarHelper.fill > 0.0f)
		{
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	public int StartTimedAction()
	{	
		TimerBarHelper.fill = 1.0f;
		return 0;
	}
	
	IEnumerator TimedAction (float myTime)
	{		
		while (TimerBarHelper.fill > 0.0f)
		{			
			yield return new WaitForSeconds(0.01f);
			TimerBarHelper.fill -= 1.0f / myTime *  Time.deltaTime;
		}	
	}
}
