using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlant : MonoBehaviour
{  
	  public GameObject plant;
    
    
    // Start is called before the first frame update
    void Start()
    {      
        Vector3 Loc1 = new Vector3 (316, 100, 346);  
        Vector3 Loc2 = new Vector3 (319, 100, 348); 
        Vector3 Loc3 = new Vector3 (312, 100, 343); 
        Vector3 Loc4 = new Vector3 (314, 100, 346);
        Vector3 Loc5 = new Vector3 (316, 100, 342);
        Vector3 Loc6 = new Vector3 (318, 100, 345);
        Vector3[] locations = new Vector3[] { Loc1, Loc2, Loc3, Loc4, Loc5, Loc6 };

        for(int i=0; i < 3; i++) {          
          int rnd = Random.Range(0,locations.Length);
          var newPlant = Instantiate(
              plant, 
              new Vector3(locations[rnd].x, locations[rnd].y, locations[rnd].z), 
              Quaternion.identity
          );
          newPlant.transform.localScale = new Vector3(4,4,4);
          locations = removeAtIndex(locations, rnd);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     Vector3[] removeAtIndex(Vector3[] inputArray, int index){
        Vector3[] outputArray = new Vector3[inputArray.Length-1];
        for(int i=0; i < index; i++){
            outputArray[i] = inputArray[i];
        }        
        
        for(int writeLoc=index; writeLoc < outputArray.Length; writeLoc++){
            outputArray[writeLoc] = inputArray[writeLoc+1];
        }
        return outputArray;
    }
}
