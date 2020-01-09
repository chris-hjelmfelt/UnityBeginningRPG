using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
	bool isFocus = false;
	bool hasInteracted = false;
	Transform player;
	public Transform interactionTransform;
	
	
	public virtual void Interact() // virtual means that this method can be overridden with other functionality in other places
	{
		//print("Interacting with " + interactionTransform.name);
		PlayerController.RemoveFocus();
	}
	
	protected virtual void Update()
	{ 
		//print("Interactable update - isFocus == " + isFocus + " and transform is " + interactionTransform.name);
		if (isFocus && !hasInteracted)
		{
			//print("Interactable - isFocus && !hasInteracted");
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}
	
	public void OnFocused (Transform playerTransform)
	{
		//print("Interactable - onFocused");
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
		//print("Interactable OnFocused - isFocus == " + isFocus);
	}
	
	public void OnDefocused ()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}
	
	void OnDrawGizmosSelected() 
	{
		if (interactionTransform == null)
				interactionTransform = transform;
			
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}
