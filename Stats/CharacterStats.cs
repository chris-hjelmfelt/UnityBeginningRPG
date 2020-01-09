
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth {get; private set;}  // any script can get this value but only this script can set it
    public Stat damage;
	public Stat armor;
	public float healthRegenSpeed = 2f;
	public int healthRegenAmount = 2;
	float lastRegenTime;
	
	public event System.Action<int,int> OnHealthChanged;
	
	void Awake ()
	{
		currentHealth = maxHealth;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			TakeDamage(10);	
		}
		
		if (Time.time > lastRegenTime + healthRegenSpeed)
		{
			RegenHealth();
		}
		
	}
	
	
	public void TakeDamage (int damage) 
	{
		int damageRandom = Random.Range(0, 3);

		damage = damage + damageRandom - armor.GetValue();
		damage = Mathf.Clamp(damage, 0 , int.MaxValue);  // don't let damage go negative
		
		currentHealth -= damage;
		print(transform.name + " takes " + damage + " damage.");
		
		if (OnHealthChanged != null)
		{
				OnHealthChanged(maxHealth, currentHealth);
		}
		
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	
	public void RegenHealth()
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += healthRegenAmount;
			currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);  // don't let health go over maxHealth
			lastRegenTime = Time.time;
		
			if (OnHealthChanged != null)
			{
					OnHealthChanged(maxHealth, currentHealth);
			}	
		}			
	}
	
	public virtual void Die ()
	{
		// Player or Creature dies. This is meant to be changable for each.
		print(transform.name + " died.");
	}
}
