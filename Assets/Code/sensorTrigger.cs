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
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == true)
        {
            caseSwtich = gameObject.name;
        }
        

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


            case "codesys1StartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StartInduction = true;
                break;
            case "codesys1StopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = true;
                break;
            case "codesys1EndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1EndInduction = true;
                break;
            case "codesys1ToRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ToRobotino = true;
                Debug.Log("triggered!!!!");
                break;
            case "codesys1FromRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1FromRobotino = true;
                break;

            case "robotinoIslandSensor":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor = true;
                GameObject.Find("robotino").GetComponent<robotinoScript>().robotinoIslandSensor = true;
                break;
            case "robotinoCarrierStop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop = true;
                break;
            case "firstIslandRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().firstIslandRobotino = true;
                break;
            case "secondIslandRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().secondIslandRobotino = true;
                break;

            case "magBackStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackStartInduction2 = true;
                break;
            case "magBackStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackStopInduction2 = true;
                Debug.Log("magback stop triggered");
                break;
            case "magBackEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackEndInduction2 = true;
                break;

            case "pressStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressStartInduction2 = true;
                break;
            case "pressStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressStopInduction2 = true;
                break;
            case "pressEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressEndInduction2 = true;
                break;
            case "pressWorkpieceCheck2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressWorkpieceCheck = true;
                break;

            case "heatingStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingStartInduction2 = true;
                break;
            case "heatingStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingStopInduction2 = true;
                break;
            case "heatingEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingEndInduction2 = true;
                break;

            case "codesys2StopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2StopInduction2 = true;
                break;
            case "codesys2ToRobotino2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2ToRobotino2 = true;
                break;
            case "codesys2FromRobotino2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2FromRobotino2 = true;
                break;

            case "magBackTop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackTop = true;
                break;
            case "magBackBottom":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackBottom = true;
                break;
        }



    }

    void OnTriggerExit(Collider other)
    {
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == true)
        {
            caseSwtich = gameObject.name;
        }

        switch (caseSwtich)
        {
            case "magFrontStartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStartInduction = false;
                break;
            case "magFrontStopInduction":
                Debug.Log(other.name);
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


            case "codesys1StartInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StartInduction = false;
                break;
            case "codesys1StopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = false;
                break;
            case "codesys1EndInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1EndInduction = false;
                break;
            case "codesys1ToRobotinoStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ToRobotino = false;
                break;
            case "codesys1FromRobotinoStopInduction":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1FromRobotino = false;
                break;

            case "robotinoIslandSensor":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor = false;
                GameObject.Find("robotino").GetComponent<robotinoScript>().robotinoIslandSensor = false;
                break;
            case "robotinoCarrierStop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop = false;
                break;
            case "firstIslandRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().firstIslandRobotino = false;
                break;
            case "secondIslandRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().secondIslandRobotino = false;
                break;

            case "magBackStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackStartInduction2 = false;
                break;
            case "magBackStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackStopInduction2 = false;
                Debug.Log("magback stop");
                break;
            case "magBackEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackEndInduction2 = false;
                break;

            case "pressStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressStartInduction2 = false;
                break;
            case "pressStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressStopInduction2 = false;
                break;
            case "pressEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressEndInduction2 = false;
                break;
            case "pressWorkpieceCheck2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressWorkpieceCheck = false;
                break;

            case "heatingStartInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingStartInduction2 = false;
                break;
            case "heatingStopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingStopInduction2 = false;
                break;
            case "heatingEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().heatingEndInduction2 = false;
                break;

            case "codesys2StopInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2StopInduction2 = false;
                break;
            case "codesys2ToRobotino2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2ToRobotino2 = false;
                break;
            case "codesys2FromRobotino2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2FromRobotino2 = false;
                break;

            case "magBackTop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackTop = false;
                break;
            case "magBackBottom":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackBottom = false;
                break;
        }



    }
}
