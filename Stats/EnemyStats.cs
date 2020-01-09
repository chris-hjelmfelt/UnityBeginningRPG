using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
	public GameObject itemDrop;
	private Vector3 armorRot = new Vector3(0.0f, 0.0f, 0.0f);
	
	public override void Die()
	{
	   base.Die();
	   
	   // death animation
	   Instantiate(itemDrop, gameObject.transform.position, Quaternion.Euler(armorRot));
	   Destroy(gameObject);
	}
}
