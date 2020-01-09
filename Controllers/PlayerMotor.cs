using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{	
	NavMeshAgent agent;
	Transform target;
	
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	
	void Update ()
	{	
		if (target != null)
		{
			agent.SetDestination(target.position);
			FaceTarget();
		}
	}

    public void MoveToPoint (Vector3 point) 
	{
		agent.SetDestination(point);		  
	}
	
	
	 public void ClearPath () 
	{
		agent.ResetPath();
		  
	}
	
	public void FollowTarget (Interactable newTarget)
	{
		agent.stoppingDistance = newTarget.radius * .4f;
		agent.updateRotation = false;
		target = newTarget.interactionTransform;
	}
	
	public void StopFollowingTarget ()
	{
		agent.stoppingDistance = 0;
		agent.updateRotation = true;
		target = null;
	}
	
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;  // rotation to target
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));  // how to rotate the player (no change in y direction)
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); //5f is a speed - smoothly move towards the direction of target
	}
}
