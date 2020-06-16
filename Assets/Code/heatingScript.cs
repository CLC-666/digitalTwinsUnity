using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool run = false;
    float startTime;
    public float heatingSpeed = 0.2f;
    public int caseSwitch = 0;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (caseSwitch)
        {
            case 0:
                if (run == true)
                {
                    caseSwitch = 10;
                }
                break;

            case 10:

                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingCurrentTemp += heatingSpeed;
                Debug.Log("Current Temperature: " + GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingCurrentTemp);
                
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingCurrentTemp >= GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingHeatTemp)
                {
                    caseSwitch = 20;
                }
                break;

            case 20:
                StartCoroutine(waitFunction());
                startTime = Time.time;
                caseSwitch = 21;
                break;

            case 21:
                Debug.Log("Heating time: " + (Time.time - startTime) + " seconds");
                break;

            case 22:
                run = false;
                break;

        }
        
    }
    
    IEnumerator waitFunction()
    {
        yield return new WaitForSeconds(GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingHeatTime);
        caseSwitch += 1;
    }
}
