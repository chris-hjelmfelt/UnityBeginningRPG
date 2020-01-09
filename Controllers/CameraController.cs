using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public Vector3 closeOffset;
	public Transform focalPoint;
	private float currentZoom = 5f; 
	public float pitch;	
	public float zoomSpeed = 4f;
	public float minZoom = -1f;
	public float maxZoom = 12f;	
	private float currentYaw = 0f;
	private float yawSpeed = 100f;
	private float currentPitch = 0f;
	private float pitchSpeed = 30f;
	
	void Update () {
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);		
		currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
		currentPitch += Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime;
		currentPitch = Mathf.Clamp(currentPitch, 0f, 1.7f);
	}
	
    void LateUpdate()
    {
		if (currentZoom < 4)
		{
			transform.position = target.position - closeOffset;			
			transform.eulerAngles = target.eulerAngles;
			transform.LookAt(focalPoint.position + new Vector3(0f,currentPitch,0f));			
			
		}else{
			transform.position = target.position - offset * currentZoom;
			transform.LookAt(target.position + Vector3.up * pitch);
			// rotation
			transform.RotateAround(target.position, Vector3.up, currentYaw);
		}
		
    }
}
