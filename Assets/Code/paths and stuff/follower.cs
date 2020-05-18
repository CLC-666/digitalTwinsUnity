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

    //A LAP IS THIS LONG 3.753859.
    public PathCreator firstIsland;
    public bool goStop = false;
    public float pauseTime = 50;
    public int counter = 0;
    bool stopper = false;
    public bool fallingEdge = false;
    public float distanceTravelled;
    public float percentLap;

    public int carrierID;
    public int spawnLocation;

    private void Start()
    {
        if (spawnLocation == 1) { percentLap = 67; }
        if (spawnLocation == 2) { percentLap = 92.5f; }
        if (spawnLocation == 3) { percentLap = 17; }
        if (spawnLocation == 4) { percentLap = 36.5f; }
    }

    void Update()
    {
        if (percentLap == 100) {percentLap = 0; }
        if (goStop == true){percentLap += 0.25f;}

        distanceTravelled = ((percentLap / 100) * -3.753859f);
        transform.position = firstIsland.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);

        pauseAtStopper();


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
            if (counter == pauseTime)
            {
                goStop = true;
            }
        }

        if (magFrontStopInduction == false && manualStopInduction == false && camInspectStopInduction == false && codesys1StopInduction == false)
        {
            counter = 0;
        }

      
    }
}