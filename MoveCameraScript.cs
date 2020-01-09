using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
	public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
	private bool debounce;
	private int count;
	
    void Update () 
	{	
		#if UNITY_EDITOR
			if (Input.GetKey("space"))
			{
				 Cursor.lockState = CursorLockMode.None;
			}else{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = true;
			}
		#endif		
	 
		#if UNITY_STANDALONE	
			//Cursor.lockState = CursorLockMode.Confined;
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);	
			
			count = count + 1;
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			if (count == 30) {			
				//print(Input.mousePosition);
				count = 0;
			}
			if (Input.mousePosition.x == 0) {	
				yaw -= speedH;		
				transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);	
			}else if (Input.mousePosition.x > 1000) {	
				yaw += speedH;			 
				transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);		
			}else{	
				transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);	
			}
			
		#endif
		
	   }
}
