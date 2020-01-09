using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	public static Interactable focus;
	public LayerMask movementMask;
	public float movementSpeed;
	public Camera cam;
	static PlayerMotor motor;	
	
 
    void Start()
    {
		motor = GetComponent<PlayerMotor>();
    }

   
    void Update()
    {
		if (EventSystem.current.IsPointerOverGameObject())  // Keep player from moving around when clicking on menus
			return;
		
		/*  Old movement mechanics - move to point clicked
        if (Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 100, movementMask)) {				
				motor.MoveToPoint(hit.point); // Move player				
				RemoveFocus(); // Stop focusing objects
			}
		}
		*/
		
		// New movement mechanics - using WASD
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey ("w")) { //run
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
		}	else if (Input.GetKey ("w") && !Input.GetKey (KeyCode.LeftShift)) {
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
		}	else if (Input.GetKey ("s")) {
			transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
		}
		if (Input.GetKey ("a") && !Input.GetKey ("d")) {
			transform.position += transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
		} else if (Input.GetKey ("d") && !Input.GetKey ("a")) {
			transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
		}		
		transform.eulerAngles = new Vector3(0.0f, cam.transform.eulerAngles.y, 0.0f);
		
		
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 100)) {
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if(interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}
    }
	
	
	void SetFocus (Interactable newFocus) 
	{
		//print("PlayerController - SetFocus");
		if(newFocus != focus)
		{
			if (focus != null)
				focus.OnDefocused();
			
			focus = newFocus;
			//motor.FollowTarget(newFocus);
		}
		newFocus.OnFocused(transform);
		
	}
	
	
	public static void RemoveFocus () 
	{	
		if (focus != null)
			focus.OnDefocused();
		
		focus = null;
		motor.StopFollowingTarget();
		motor.ClearPath();
	}
}
