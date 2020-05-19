using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class follower : MonoBehaviour
{
    public bool manualStartInduction = false;
    public bool manualStopInduction = false; //SPAWN LOCATION 2 = follower: 92%.
    public bool manualEndInduction = false;
    public bool camInspectStartInduction = false;
    public bool camInspectStopInduction = false; //SPAWN LOCATION 3 = follower: 17%.
    public bool camInspectEndInduction = false;
    public bool codesys1StopInduction = false; //SPAWN LOCATION 4 = follower: 36.5%.
    public bool magFrontStartInduction = false;
    public bool magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
    public bool magFrontEndInduction = false;
    public bool parkedOnRobotino = false;

    //A FIRSTISLAND LAP IS THIS LONG 3.753859.
    //A TOROBOTINO LATP IS THIS LONG 1.174999.
    public PathCreator firstIsland;
    public PathCreator toRobotino;
    public int pathMode = 0;
    public bool goStop = false;
    float pauseTime = 50;
    int counter = 0;
    public float distanceTravelled;
    public float distanceTravelledtoRobot;
    public float percentLap;
    public float toRobotPercentLap;
    public int caseSwitch = 0;
    public int spawnLocation;
    int currentLocation;

    public int carrierID;
    int productCode;
    float targetTemp;
    float heatingTime;
    float pressure;

    public bool busy = false;
    public bool order = true;


    private void Start()
    {
        if (spawnLocation == 1) { percentLap = 67; }
        if (spawnLocation == 2) { percentLap = 92.5f; }
        if (spawnLocation == 3) { percentLap = 17; }
        if (spawnLocation == 4) { percentLap = 36.5f; }
    }

    void Update()
    {
        if (pathMode == 0)
        {
            if (percentLap == 100) { percentLap = 0; }
            if (goStop == true) { percentLap += 0.25f; }

            distanceTravelled = ((percentLap / 100) * -3.753859f);
            transform.position = firstIsland.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelled);
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
        }

        if (pathMode == 1)
        {
            if (goStop == true && toRobotPercentLap < 100) { toRobotPercentLap += 0.25f; }

            distanceTravelledtoRobot = ((toRobotPercentLap / 100) * 1.174999f);
            transform.position = toRobotino.path.GetPointAtDistance(distanceTravelledtoRobot, EndOfPathInstruction.Stop);
            transform.rotation = toRobotino.path.GetRotationAtDistance(distanceTravelledtoRobot, EndOfPathInstruction.Stop);
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);          
        }

        pauseAtStopper();

        location();

        mainOrderStructure();


    }

    void OnTriggerEnter(Collider other)
    {
        string caseSwitch;
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "magFrontStartInduction":
                magFrontStartInduction = true;
                break;
            case "magFrontStopInduction":
                magFrontStopInduction = true;
                break;
            case "magFrontEndInduction":
                magFrontEndInduction = true;
                break;


            case "manualStartInduction":
                manualStartInduction = true;
                break;
            case "manualStopInduction":
                manualStopInduction = true;
                break;
            case "manualEndInduction":
                manualEndInduction = true;
                break;


            case "camInspectStartInduction":
                camInspectStartInduction = true;
                break;
            case "camInspectStopInduction":
                camInspectStopInduction = true;
                break;
            case "camInspectEndInduction":
                camInspectEndInduction = true;
                break;


            case "codesys1StopInduction":
               codesys1StopInduction = true;
                break;


            case "parkedOnRobotino":
                parkedOnRobotino = false;
                break;
        }



    }

    void OnTriggerExit(Collider other)
    {
        string caseSwitch;
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "magFrontStartInduction":
                magFrontStartInduction = false;
                break;
            case "magFrontStopInduction":
                magFrontStopInduction = false;
                break;
            case "magFrontEndInduction":
                magFrontEndInduction = false;
                break;


            case "manualStartInduction":
                manualStartInduction = false;
                break;
            case "manualStopInduction":
                manualStopInduction = false;
                break;
            case "manualEndInduction":
                manualEndInduction = false;
                break;


            case "camInspectStartInduction":
                camInspectStartInduction = false;
                break;
            case "camInspectStopInduction":
                camInspectStopInduction = false;
                break;
            case "camInspectEndInduction":
                camInspectEndInduction = false;
                break;


            case "codesys1StopInduction":
                codesys1StopInduction = false;
                break;
        }



    }

    void pauseAtStopper()
    {
        if (magFrontStopInduction == true || manualStopInduction == true || camInspectStopInduction == true || codesys1StopInduction == true)
        {
            if (counter < pauseTime) { goStop = false; }
            counter++;
            if (counter >= pauseTime && busy == false)
            {
                goStop = true;
            }
        }

        if (magFrontStopInduction == false && manualStopInduction == false && camInspectStopInduction == false && codesys1StopInduction == false)
        {
            counter = 0;
        }
    }

    void location()
    {
        if(percentLap <= 67 && percentLap > 38f ) { currentLocation = 1; }
        if (percentLap <= 92.5f && percentLap > 69) { currentLocation = 2; }
        if (percentLap <= 17f || percentLap > 94f) { currentLocation = 3; }
        if (percentLap <= 36.5f && percentLap > 19) { currentLocation = 4; }
    }

    void mainOrderStructure()
    {
        switch (caseSwitch) //each case should be a target station. If it meets the wrong one, move on.
        {
            case 0: //magFront

                if (currentLocation == 1 && goStop == false && order == true)
                {
                    busy = true;

                    GameObject.Find("magFrontLift").GetComponent<magFrontLiftScript>().run = true;
                    GameObject.Find("magFrontLift").GetComponent<magFrontLiftScript>().carrierID = carrierID;
                    caseSwitch = 1;
                }
                break;

            case 1:

                if (GameObject.Find("magFrontLift").GetComponent<magFrontLiftScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 20;
                }
                break;

            case 20: //manual
                if (currentLocation == 2 && goStop == false && order == true)
                {
                    busy = true;
                    GameObject.Find("manualConveyor").GetComponent<manualPlaceScript>().run = true;
                    GameObject.Find("manualConveyor").GetComponent<manualPlaceScript>().carrierID = carrierID;
                    caseSwitch = 21;
                }
                break;

            case 21:
                if (GameObject.Find("manualConveyor").GetComponent<manualPlaceScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 30;
                }
                break;

            case 30:
                if (currentLocation == 3 && goStop == false && order == true)
                {
                    busy = true;
                    GameObject.Find("camInspectConveyor").GetComponent<camInspectScript>().run = true;
                    GameObject.Find("camInspectConveyor").GetComponent<camInspectScript>().carrierID = carrierID;
                    caseSwitch = 31;
                }
                break;

            case 31:
                if (GameObject.Find("camInspectConveyor").GetComponent<camInspectScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 40;
                }
                break;

            case 40:
                if (currentLocation == 4 && goStop == false && order == true)
                {
                    pathMode = 1;
                }
                break;
        }

    }

}