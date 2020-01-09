using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Buildable : Interactable  // This inherits everything from the interactable script
{
	
	public int planksNeeded;
	public int stonesNeeded;
	bool finished = false;		
	public GameObject timerBar;
	Image timerSlider;	
	public GameObject startSite;
	public GameObject endSite;
	
	
    public override void Interact()
	{
		base.Interact();
		Build();
	}	
	
	void Start()
	{
		timerSlider = timerBar.transform.GetChild(0).GetComponent<Image>();
		startSite.SetActive(true);
		endSite.SetActive(false);
	}
	
	protected override void Update()
	{		
		base.Update();
		timerSlider.fillAmount = TimerBarHelper.fill;	// may need a master fill amount
	}
	
	
   	void Build()
	{
		print("Buildable - Trying to build");
		
		if (finished == false){
			if (EquipmentManager.equippedTool.name == "Hammer")
			{
				for (int i = 0; i < InventoryUI.inventory.items.Count; i++)
				{			
					if(InventoryUI.inventory.items[i].name == "Plank" && planksNeeded != 0)
					{
						StartCoroutine(BuildSequence(InventoryUI.inventory.items[i], 5.0f));
						break;
					}else if (InventoryUI.inventory.items[i].name == "Stone" && stonesNeeded != 0){
						StartCoroutine(BuildSequence(InventoryUI.inventory.items[i], 5.0f));
						break;
					}
					MessagesUI.SetMessage("Still Needed: Stones - " + stonesNeeded + " Planks - " + planksNeeded);
				}			
			} else {
				print("Equip a Hammer");
			}
		} else{
			print("Finished");
		}
		
	}
	
	IEnumerator BuildSequence(Item toRemove, float myTime)  // this is a coroutine - a sequence that prevents certain items from happening until others have finished
	{
		Inventory.instance.Remove(toRemove);
		MessagesUI.SetMessage("Building");
		yield return waitForTimer();
		yield return StartTimedAction();
		yield return TimedAction(myTime);
		if(toRemove.name == "Plank"){
			planksNeeded = planksNeeded -1;
		}else{
			stonesNeeded = stonesNeeded -1;
		}
		if ((planksNeeded == 0) && (stonesNeeded == 0)) {
			finished = true;
			print("finished Building");
			startSite.SetActive(false);
			endSite.SetActive(true);
		}else{
			print("Planks needed: " + planksNeeded + " and Stones needed: " + stonesNeeded);
		}
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
