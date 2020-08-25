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
    public bool codesys1ToRobotino = false;
    public bool robotinoCarrierStop = false;
    public bool codesys1FromRobotino = false;
    public bool magFrontStartInduction = false;
    public bool magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
    public bool magFrontEndInduction = false;
    public bool magBackStartInduction2 = false;
    public bool magBackStopInduction2 = false;
    public bool magBackEndInduction2 = false;
    public bool pressStartInduction2 = false;
    public bool pressStopInduction2 = false;
    public bool pressEndInduction2 = false;
    public bool heatingStartInduction2 = false;
    public bool heatingStopInduction2 = false;
    public bool heatingEndInduction2 = false;
    public bool codesys2StopInduction2 = false;
    public bool codesys2ToRobotino2 = false;
    public bool codesys2FromRobotino2 = false;


    public bool simMode = false;
    public runInSimMode varSource;

    //A FIRSTISLAND LAP IS THIS LONG 3.753859.
    //A TOROBOTINO LATP IS THIS LONG 1.174999.
    public PathCreator firstIsland;
    public PathCreator secondIsland;
    public PathCreator firstRobotino;
    public PathCreator secondRobotino;
    public int pathMode = 1;
    public bool goStop = false;
    float pauseTime = 50;
    int counter = 0;
    public float distanceTravelledFirstIsland;
    public float distanceTravelledSecondIsland;
    public float distanceTravelledFirstRobotinoLap;
    public float distanceTravelledSecondRobotinoLap;
    public float percentLapFirstIsland;
    public float percentLapSecondIsland;
    public float toRobotPercentLap;
    public float percentSecondIslandRobotinoLap;
    public int caseSwitch = 0;
    public int spawnLocation;
    public int currentLocation;
    public bool initSecondIslandRobotinoLap = false;
    public bool initSecondIslandLap = false;

    public int carrierID;
    int productCode;
    float targetTemp;
    float heatingTime;
    float pressure;

    public bool busy = false;
    public bool order = false;


    private void Start()
    {
        if (spawnLocation == 1) { percentLapFirstIsland = 67; }
        if (spawnLocation == 2) { percentLapFirstIsland = 92.5f; }
        if (spawnLocation == 3) { percentLapFirstIsland = 17; }
        if (spawnLocation == 4) { percentLapFirstIsland = 36.5f;}
        if (spawnLocation == 5) { percentLapSecondIsland = 67.15f; pathMode = 4; }
        if (spawnLocation == 6) { percentLapSecondIsland = 92.15f; pathMode = 4; }
        if (spawnLocation == 7) { percentLapSecondIsland = 17.25f; pathMode = 4; }
        if (spawnLocation == 8) { percentLapSecondIsland = 36.75f; pathMode = 4; }

    }

    void Update()
    {
        varUpdate(); //Get all updated sensor values;

        switch (pathMode) {
            case 1: //first island
                if (percentLapFirstIsland == 100) { percentLapFirstIsland = 0; }
                if (goStop == true) { percentLapFirstIsland += 0.25f; }

                distanceTravelledFirstIsland = ((percentLapFirstIsland / 100) * -3.753859f);
                transform.position = firstIsland.path.GetPointAtDistance(distanceTravelledFirstIsland);
                transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelledFirstIsland);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;

            case 2: //first island robotino drop off lap
        
                if (goStop == true && toRobotPercentLap < 100) { toRobotPercentLap += 0.25f; }

                distanceTravelledFirstRobotinoLap = ((toRobotPercentLap / 100) * 1.8f);
                transform.position = firstRobotino.path.GetPointAtDistance(distanceTravelledFirstRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = firstRobotino.path.GetRotationAtDistance(distanceTravelledFirstRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
                break;


            case 3: //second island robotino drop off lap
        
                if (initSecondIslandRobotinoLap == false) { percentSecondIslandRobotinoLap = toRobotPercentLap; initSecondIslandRobotinoLap = true; }
                if (goStop == true && percentSecondIslandRobotinoLap < 100) { percentSecondIslandRobotinoLap += 0.25f; }

                distanceTravelledSecondRobotinoLap = ((percentSecondIslandRobotinoLap / 100) * 1.8f);
                transform.position = secondRobotino.path.GetPointAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = secondRobotino.path.GetRotationAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;


            case 4:
                if (percentLapSecondIsland == 100.15f) { percentLapSecondIsland = 0; }
                initSecondIslandRobotinoLap = false;
                if (varSource.codesys2StopInduction2 == true && varSource.codesys2ToRobotino2 == false) { pathMode = 5; }
                if (percentLapSecondIsland == 100) { percentLapSecondIsland = 0; }
                if (goStop == true) { percentLapSecondIsland += 0.25f; }

                distanceTravelledSecondIsland = ((percentLapSecondIsland / 100) * -3.753859f);
                transform.position = secondIsland.path.GetPointAtDistance(distanceTravelledSecondIsland);
                transform.rotation = secondIsland.path.GetRotationAtDistance(distanceTravelledSecondIsland);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;

            case 5:
                if (initSecondIslandRobotinoLap == false) { percentSecondIslandRobotinoLap = 0; initSecondIslandRobotinoLap = true; }
                if (goStop == true && percentSecondIslandRobotinoLap < 100) { percentSecondIslandRobotinoLap += 0.25f; }

                distanceTravelledSecondRobotinoLap = ((percentSecondIslandRobotinoLap / 100) * 1.8f);
                transform.position = secondRobotino.path.GetPointAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = secondRobotino.path.GetRotationAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
                break;
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
            case "codesys1ToRobotino":
                codesys1ToRobotino = true;
                Debug.Log(distanceTravelledFirstRobotinoLap);
                break;
            case "codesys1FromRobotino":
                codesys1FromRobotino = true;
                break;

            case "robotinoCarrierStop":
                robotinoCarrierStop = true;
                break;

            case "magBackStartInduction2":
                magBackStartInduction2 = true;
                break;
            case "magBackStopInduction2":
                magBackStopInduction2 = true;
                break;
            case "magBackEndInduction2":
                magBackEndInduction2 = true;
                break;

            case "pressStartInduction2":
                pressStartInduction2 = true;
                break;
            case "pressStopInduction2":
                pressStopInduction2 = true;
                break;
            case "pressEndInduction2":
                pressEndInduction2 = true;
                break;

            case "heatingStartInduction2":
                heatingStartInduction2 = true;
                break;
            case "heatingStopInduction2":
                heatingStopInduction2 = true;
                break;
            case "heatingEndInduction2":
                heatingEndInduction2 = true;
                break;

            case "codesys2StopInduction2":
                codesys2StopInduction2 = true;
                break;
            case "codesys2ToRobotino2":
                codesys2ToRobotino2 = true;
                break;
            case "codesys2FromRobotino2":
                codesys2FromRobotino2 = true;
                break;

        }
        if (other.gameObject.name.Contains("robotinoC"))
        {
            Debug.Log("collision " + other.gameObject.name);
            //carrier = other.gameObject;
            gameObject.transform.parent = GameObject.Find("robotino").transform.transform;     //carrier.transform;
            pathMode = 10;
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
            case "codesys1ToRobotino":
                codesys1ToRobotino = false;
                break;
            case "codesys1FromRobotino":
                codesys1FromRobotino = false;
                break;

            case "robotinoCarrierStop":
                robotinoCarrierStop = false;
                break;

            case "magBackStartInduction2":
                magBackStartInduction2 = false;
                break;
            case "magBackStopInduction2":
                magBackStopInduction2 = false;
                break;
            case "magBackEndInduction2":
                magBackEndInduction2 = false;
                break;

            case "pressStartInduction2":
                pressStartInduction2 = false;
                break;
            case "pressStopInduction2":
                pressStopInduction2 = false;
                break;
            case "pressEndInduction2":
                pressEndInduction2 = false;
                break;

            case "heatingStartInduction2":
                heatingStartInduction2 = false;
                break;
            case "heatingStopInduction2":
                heatingStopInduction2 = false;
                break;
            case "heatingEndInduction2":
                heatingEndInduction2 = false;
                break;

            case "codesys2StopInduction2":
                codesys2StopInduction2 = false;
                break;
            case "codesys2ToRobotino2":
                codesys2ToRobotino2 = false;
                break;
            case "codesys2FromRobotino2":
                codesys2FromRobotino2 = false;
                break;
        }



    }

    void pauseAtStopper()
    {
        if (magFrontStopInduction == true || manualStopInduction == true || camInspectStopInduction == true || codesys1StopInduction == true || codesys1ToRobotino == true || codesys1FromRobotino == true || robotinoCarrierStop == true || magBackStopInduction2 == true || pressStopInduction2 == true || heatingStopInduction2 == true || codesys2StopInduction2 == true || codesys2ToRobotino2 == true || codesys2FromRobotino2 == true)
        {
            if (counter < pauseTime) { goStop = false; }
            counter++;
            if (counter >= pauseTime && busy == false)
            {
                goStop = true;
            }
        }

        if (magFrontStopInduction == false && manualStopInduction == false && camInspectStopInduction == false && codesys1StopInduction == false && codesys1ToRobotino == false && codesys1FromRobotino == false && robotinoCarrierStop == false && magBackStopInduction2 == false && pressStopInduction2 == false && heatingStopInduction2 == false && codesys2StopInduction2 == false && codesys2FromRobotino2 == false && codesys2ToRobotino2 == false)
        {
            counter = 0;
        }
    }

    void location()
    {
        if (percentLapFirstIsland <= 67 && percentLapFirstIsland > 38f && pathMode == 1) { currentLocation = 1; }
        if (percentLapFirstIsland <= 92.5f && percentLapFirstIsland > 69 && pathMode == 1) { currentLocation = 2; }
        if (percentLapFirstIsland <= 17f || percentLapFirstIsland > 94f) { if (pathMode == 1) { currentLocation = 3; } }
        if (percentLapFirstIsland <= 36.5f && percentLapFirstIsland > 19 && pathMode == 1) { currentLocation = 4; }
        if (toRobotPercentLap <= 13.25f && percentLapFirstIsland > 35 && pathMode == 2) { currentLocation = 5; }
        if (toRobotPercentLap <= 60.75f && toRobotPercentLap > 13.25f && pathMode == 2) { currentLocation = 6; }
        if (toRobotPercentLap <= 84.25f && toRobotPercentLap > 61f && pathMode == 2) { currentLocation = 7; }
        if (pathMode == 3) { currentLocation = 8; }
        if (percentSecondIslandRobotinoLap == 100 && pathMode == 3) { currentLocation = 9; }
        if (percentLapSecondIsland <= 67 && percentLapSecondIsland > 38f && pathMode == 4) { currentLocation = 10; }
        if (percentLapSecondIsland <= 92.5f && percentLapSecondIsland > 69 && pathMode == 4) { currentLocation = 11; }
        if (percentLapSecondIsland <= 17f || percentLapSecondIsland > 94f) { if (pathMode == 4) { currentLocation = 12; } }
        if (percentLapSecondIsland <= 36.5f && percentLapSecondIsland > 19 && pathMode == 4) { currentLocation = 13; }
        if (percentSecondIslandRobotinoLap >= 60 && robotinoCarrierStop == true) { currentLocation = 14; }
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
                if (currentLocation == 4 && goStop == true && order == true)
                {
                    if (percentLapFirstIsland >= 37) { caseSwitch = 41; }
                    
                }
                break;

            case 41:
                pathMode = 2;
                caseSwitch = 50;
                break;

            case 50:
                if (currentLocation == 5 && goStop == false && order == true)
                {
                    busy = true;
                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor == true)
                    {
                        busy = false;
                        caseSwitch = 60;
                    }
                }
                break;
            
            case 60:
                if (currentLocation == 6 && order == true)
                {
                    busy = true;
                    caseSwitch = 61;
                }
                break;

            case 61:
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland2 = true;
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland1 = false;
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor == false)
                {
                    caseSwitch = 62;
                }
                break;
            
            case 62:
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor == true) //drop carrier off
                {
                    busy = false;
                    caseSwitch = 63;
                }
                break;

            case 63: //removing parent (robotino)
                transform.parent = null;
                caseSwitch = 70;
                break;


            case 70:
                pathMode = 3;
                if (currentLocation == 9 && order == true)
                {
                    pathMode = 4;
                    percentLapSecondIsland = 48.4f;
                    caseSwitch = 80;

                }
                break;

            case 80:
                if (currentLocation == 10 && goStop == false && order == true)
                {
                    busy = true;
                    GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland2 = false;
                    GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland1 = false;
                    GameObject.Find("magBackLift").GetComponent<magBackLiftScript>().run = true;
                    GameObject.Find("magBackLift").GetComponent<magBackLiftScript>().carrierID = carrierID;
                    caseSwitch = 81;
                }
                break;
            case 81:
                if (GameObject.Find("magBackLift").GetComponent<magBackLiftScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 90;
                }
                break;

            case 90:
                if (currentLocation == 11 && goStop == false && order == true)
                {
                    busy = true;
                    GameObject.Find("press").GetComponent<pressScript>().run = true;
                    caseSwitch = 95;
                }
                    break;

            case 95:
                if (GameObject.Find("press").GetComponent<pressScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 100;
                }
                break;

            case 100:
                if (currentLocation == 12 && goStop == false && order == true)
                {
                    busy = true;
                    GameObject.Find("heating").GetComponent<heatingScript>().run = true;
                    caseSwitch = 105;
                }
                break;
            case 105:
                if (GameObject.Find("heating").GetComponent<heatingScript>().run == false)
                {
                    busy = false;
                    caseSwitch = 120;
                }
                break;

            case 120:
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland1 = false;
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland2 = false;

                if (currentLocation == 14 && robotinoCarrierStop == true)
                {
                    caseSwitch = 125;
                }
                break;

            case 125:
                busy = true;
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland1 = true;
                GameObject.Find("robotino").GetComponent<robotinoScript>().toIsland2 = false;

                if (GameObject.Find("robotino").GetComponent<robotinoScript>().turningPoint2 == true)
                {
                    caseSwitch = 127;
                }
                break;

            case 127:
                if (robotinoCarrierStop == true)
                {
                    caseSwitch = 130;
                }
                break;
            case 130:
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor == true) //drop carrier off
                {
                    busy = false;
                    caseSwitch = 135;
                }
                break;

            case 135: //removing parent (robotino)
                transform.parent = null;
                caseSwitch = 140;
                break;

            case 140:
                pathMode = 2;
                if (toRobotPercentLap == 100)
                {
                    percentLapFirstIsland = 47.5f;
                    pathMode = 1;
                    caseSwitch = 150;
                }
                break;

            case 150: //product finshed
                if (currentLocation == 2 && goStop == false && order == true)
                {
                    busy = true;
                    Debug.Log("Product finished");
                    foreach (Transform child in transform)
                    {
                        if (!child.name.Contains("Fbx")) {
                            GameObject.Destroy(child.gameObject);
                            order = false;
                            busy = false;
                            toRobotPercentLap = 0;
                            caseSwitch = 0;
                        }
                    }
                }
                break;
        } 

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    goStop = false;
    //    Debug.Log("COLLISION");
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    goStop = true;
    //}

    void varUpdate()
    {
        if (simMode == true)
        {
            varSource = GameObject.Find("Main Camera").GetComponent<runInSimMode>();
        }
    }

}