using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	public float attackSpeed = 1f;
	private float attackCooldown = 0f;
	
	// These 3 are for animation purposes
	public bool InCombat { get; private set; }  // any script can get this but only this script can set it
	const float combatCooldown = 5; // time before a character is considered not in combat 
	float lastAttackTime;
	
	public float attackDelay = .6f;	
	CharacterStats myStats;
	CharacterStats opponentStats;
	
	public event System.Action OnAttack;  // This is like a delegate with no arguments or return
	
	void Start()
	{
		myStats = GetComponent<CharacterStats>();
	}
	
	void Update()
	{
		attackCooldown -= Time.deltaTime;
		
		if (Time.time - lastAttackTime > combatCooldown)
		{
			InCombat = false;
		}
	}
	
   public void Attack(CharacterStats targetStats)
   {
	   if (attackCooldown <= 0f)
	   {			
			opponentStats =  targetStats;
			if (OnAttack != null)  // This will notify the animation controller whenever we attack
				OnAttack();
			
			attackCooldown = 1f / attackSpeed;
			InCombat = true;
			lastAttackTime = Time.time;
	   }
   }
    
   
   public void AttackHit_AnimationEvent()
   {
	   opponentStats.TakeDamage(myStats.damage.GetValue());
	   if (opponentStats.currentHealth <= 0)
	   {
		   InCombat = false;
	   }
   }
}
