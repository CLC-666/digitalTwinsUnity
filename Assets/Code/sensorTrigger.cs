using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorTrigger : MonoBehaviour
{
    bool sensorState = false;
    string caseSwtich;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        caseSwtich = gameObject.name;

        

        switch (caseSwtich)
        {
            case "magFrontStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStartInduction = true;
                break;
            case "magFrontStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStopInduction = true;
                break;
            case "magFrontEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontEndInduction = true;
                break;

            case "magFrontTop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop = true;
                break;
            case "magFrontBottom":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontBottom = true;
                break;
        

            case "manualStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStartInduction = true;
                break;
            case "manualStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStopInduction = true;
                break;
            case "manualEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualEndInduction = true;
                break;


            case "camInspectStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStartInduction = true;
                break;
            case "camInspectStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStopInduction = true;
                break;
            case "camInspectEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectEndInduction = true;
                break;


            case "codesys1StopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = true;
                break;
        }



    }

    void OnTriggerExit(Collider other)
    {
        caseSwtich = gameObject.name;

        switch (caseSwtich)
        {
            case "magFrontStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStartInduction = false;
                break;
            case "magFrontStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStopInduction = false;
                break;
            case "magFrontEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontEndInduction = false;
                break;

            case "magFrontTop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop = false;
                break;
            case "magFrontBottom":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontBottom = false;
                break;


            case "manualStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStartInduction = false;
                break;
            case "manualStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStopInduction = false;
                break;
            case "manualEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualEndInduction = false;
                break;


            case "camInspectStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStartInduction = false;
                break;
            case "camInspectStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStopInduction = false;
                break;
            case "camInspectEndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectEndInduction = false;
                break;


            case "codesys1StopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = false;
                break;
        }



    }
}
