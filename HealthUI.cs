using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{	
	public GameObject uiPrefab;
	public GameObject healthCanvas;
	public Transform target;
	float visibleTime = 5;
	float lastMadeVisibleTime;
	private int currentHealth;
	private int maxHealth;
	
	Transform ui;
	Image healthSlider;
	Transform cam;
	
	
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main.transform;
		
		// find the Health bar object and hide it until a fight
		healthCanvas = GameObject.Find("HealthBar");
		ui = Instantiate(uiPrefab, healthCanvas.transform).transform;
		healthSlider = ui.GetChild(0).GetComponent<Image>();
		ui.gameObject.SetActive(false);
		
		GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;  // subscribe to OnHealthChanged in the CharacterStats script
    }
	
	void OnHealthChanged(int maxHealth, int currentHealth)
	{
		if (ui != null)
		{
			ui.gameObject.SetActive(true);
			lastMadeVisibleTime = Time.time;  // set it to the current time
			float healthPercent = (float)currentHealth / maxHealth;
			healthSlider.fillAmount = healthPercent;
			
			if (currentHealth <= 0)
			{
				Destroy(ui.gameObject);
			}
		}
	}


    // Update is called once per frame
    void LateUpdate()  // want to be sure targets position is updated before this is called
    {
		if (ui != null)
		{
			ui.position = target.position;
			ui.forward = -cam.forward;  // align the UI with the camera view
			currentHealth = GetComponent<CharacterStats>().currentHealth;
			maxHealth = GetComponent<CharacterStats>().maxHealth;
			
			if (Time.time - lastMadeVisibleTime > visibleTime && currentHealth == maxHealth) // isn't fighting and isn't damaged
			{
				ui.gameObject.SetActive(false);
			}
		}
    }
}
