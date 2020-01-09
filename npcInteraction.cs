using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : Interactable  // This inherits everything from the interactable script
{
	public GameObject SpeachUI;	
	
    public override void Interact()
	{
		base.Interact();
		
		Speak();
	}
	
	void Speak ()
	{
		
	}
}
