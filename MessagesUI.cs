using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesUI : MonoBehaviour
{
	public GameObject messageCanvas;
	static Text[] textComponent; 
	
    // Start is called before the first frame update
    void Start()
    {				
		textComponent = messageCanvas.GetComponentsInChildren<Text>();	
        SetMessage("Welcome to the game!");
    }

    public static void SetMessage(string toPrint)
	{
		for(int k=0; k<3; k++)
		{
			textComponent[k].text = textComponent[k+1].text;
		}
		textComponent[3].text = toPrint;
	}			
}
