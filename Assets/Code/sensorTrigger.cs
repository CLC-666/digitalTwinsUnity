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
            case "codesys1ToRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ToRobotino = true;
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
                break;
            case "magBackEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackEndInduction2 = true;
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
            case "codesys1ToRobotino":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ToRobotino = false;
                break;
            case "codesys1FromRobotino":
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
                break;
            case "magBackEndInduction2":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magBackEndInduction2 = false;
                break;
        }



    }
}
