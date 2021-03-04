using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class follower : MonoBehaviour
{
    //public bool manualStartInduction = false;
    //public bool manualStopInduction = false; //SPAWN LOCATION 2 = follower: 92%.
    //public bool manualEndInduction = false;
    //public bool camInspectStartInduction = false;
    //public bool camInspectStopInduction = false; //SPAWN LOCATION 3 = follower: 17%.
    //public bool camInspectEndInduction = false;
    //public bool codesys1StopInduction = false; //SPAWN LOCATION 4 = follower: 36.5%.
    //public bool codesys1ToRobotino = false;
    //public bool robotinoCarrierStop = false;
    //public bool codesys1FromRobotino = false;
    //public bool magFrontStartInduction = false;
    //public bool magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
    //public bool magFrontEndInduction = false;
    //public bool magBackStartInduction2 = false;
    //public bool magBackStopInduction2 = false;
    //public bool magBackEndInduction2 = false;
    //public bool pressStartInduction2 = false;
    //public bool pressStopInduction2 = false;
    //public bool pressEndInduction2 = false;
    //public bool heatingStartInduction2 = false;
    //public bool heatingStopInduction2 = false;
    //public bool heatingEndInduction2 = false;
    //public bool codesys2StopInduction2 = false;
    //public bool codesys2ToRobotino2 = false;
    //public bool codesys2FromRobotino2 = false;


    public bool simMode = true;
    public float speed = 4.5f; //speedy mode 25, real speed 4.5

    //A FIRSTISLAND LAP IS THIS LONG 3.753859.
    //A TOROBOTINO LATP IS THIS LONG 1.174999.
    public PathCreator firstIsland;
    public PathCreator secondIsland;
    public PathCreator firstRobotino;
    public PathCreator secondRobotino;
    public int pathMode = 1;
    public bool goStop = false;
    public float pauseTime = 5500; //speedy mode 600, real speed 5500
    public int counter = 0;
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

    public bool convStartMagFront = false;
    public bool convStartManual = false;
    public bool convStartCamInspec = false;
    public bool convStartCodesys1 = false;

    public bool magFrontConv = false;
    public bool manualConv = false;
    public bool camInspecConv = false;
    public bool codesys1Conv = false;
    public bool codesys1OutConv = false;
    public bool codesys1InConv = false;
    public bool robotinoConv = false;

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
        switch (pathMode)
        {
            case 1: //first island
                if (percentLapFirstIsland == 100) { percentLapFirstIsland = 0; }
                if (goStop == true) { percentLapFirstIsland += speed * Time.deltaTime; }

                distanceTravelledFirstIsland = ((percentLapFirstIsland / 100) * -3.753859f);
                transform.position = firstIsland.path.GetPointAtDistance(distanceTravelledFirstIsland);
                transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelledFirstIsland);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;

            case 2: //first island robotino drop off lap

                if (goStop == true && toRobotPercentLap < 100) { toRobotPercentLap += speed * Time.deltaTime; }

                distanceTravelledFirstRobotinoLap = ((toRobotPercentLap / 100) * 1.8f);
                transform.position = firstRobotino.path.GetPointAtDistance(distanceTravelledFirstRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = firstRobotino.path.GetRotationAtDistance(distanceTravelledFirstRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
                break;


            case 3: //second island robotino drop off lap

                if (initSecondIslandRobotinoLap == false) { percentSecondIslandRobotinoLap = toRobotPercentLap; initSecondIslandRobotinoLap = true; }
                if (goStop == true && percentSecondIslandRobotinoLap < 100) { percentSecondIslandRobotinoLap += speed * Time.deltaTime; }

                distanceTravelledSecondRobotinoLap = ((percentSecondIslandRobotinoLap / 100) * 1.8f);
                transform.position = secondRobotino.path.GetPointAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = secondRobotino.path.GetRotationAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;


            case 4:
                if (percentLapSecondIsland == 100.15f) { percentLapSecondIsland = 0; }
                initSecondIslandRobotinoLap = false;
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2StopInduction2 == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys2ToRobotino2 == false) { pathMode = 5; }
                if (percentLapSecondIsland == 100) { percentLapSecondIsland = 0; }
                if (goStop == true) { percentLapSecondIsland += speed * Time.deltaTime; }

                distanceTravelledSecondIsland = ((percentLapSecondIsland / 100) * -3.753859f);
                transform.position = secondIsland.path.GetPointAtDistance(distanceTravelledSecondIsland);
                transform.rotation = secondIsland.path.GetRotationAtDistance(distanceTravelledSecondIsland);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);
                break;

            case 5:
                if (initSecondIslandRobotinoLap == false) { percentSecondIslandRobotinoLap = 0; initSecondIslandRobotinoLap = true; }
                if (goStop == true && percentSecondIslandRobotinoLap < 100) { percentSecondIslandRobotinoLap += speed * Time.deltaTime; }

                distanceTravelledSecondRobotinoLap = ((percentSecondIslandRobotinoLap / 100) * 1.8f);
                transform.position = secondRobotino.path.GetPointAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = secondRobotino.path.GetRotationAtDistance(distanceTravelledSecondRobotinoLap, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
                break;
        }
        

        //pauseAtStopper();

        location();

        mainOrderStructure();

        conveyors();
        

    }

    void OnTriggerEnter(Collider other)
    {
        string caseSwitch;
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
 
            case "magFrontConv":
                magFrontConv = true;
                break;

            case "manualConv":
                manualConv = true;
                break;
           
            case "camInspecConv":
                camInspecConv = true;
                break;

            case "codesys1Conv":
                codesys1Conv = true;
                break;

            case "codesys1OutConv":
                codesys1OutConv = true;
                break;

            case "codesys1InConv":
                codesys1InConv = true;
                break;

            case "robotinoConv":
                robotinoConv = true;
                break;

            case "robotinoCarrierStop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop = true;
                break;

        }
        if (other.gameObject.name.Contains("StopInduction"))
        {
            if (counter < pauseTime) { goStop = false; }
           
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

            case "magFrontConv":
                magFrontConv = false;
                break;

            case "manualConv":
                manualConv = false;
                break;

            case "camInspecConv":
                camInspecConv = false;
                break;

            case "codesys1Conv":
                codesys1Conv = false;
                break;

            case "codesys1OutConv":
                codesys1OutConv = false;
                break;

            case "codesys1InConv":
                codesys1InConv = false;
                break;

            case "robotinoConv":
                robotinoConv = false;
                break;

            case "robotinoCarrierStop":
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop = false;
                break;

        }

        if (other.gameObject.name.Contains("StopInduction"))
        {
            counter = 0;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("StopInduction"))
        {
            //Debug.Log(counter.ToString() + " " + gameObject.name);
            if (counter < pauseTime) { goStop = false; }
            counter++;
            if (counter >= pauseTime && busy == false)
            {
                goStop = true;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name.Contains("carrier") && goStop == true)
        //{
        //    goStop = false;
        //    Debug.Log("ahhh a collision");
        //}
        
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
        if (percentSecondIslandRobotinoLap >= 100 && pathMode == 3) { currentLocation = 9; }
        if (percentLapSecondIsland <= 67 && percentLapSecondIsland > 38f && pathMode == 4) { currentLocation = 10; }
        if (percentLapSecondIsland <= 92.5f && percentLapSecondIsland > 69 && pathMode == 4) { currentLocation = 11; }
        if (percentLapSecondIsland <= 17f || percentLapSecondIsland > 94f) { if (pathMode == 4) { currentLocation = 12; } }
        if (percentLapSecondIsland <= 36.5f && percentLapSecondIsland > 19 && pathMode == 4) { currentLocation = 13; }
        if (percentSecondIslandRobotinoLap >= 60 && GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true) { currentLocation = 14; }
    }

    void mainOrderStructure()
    {
        switch (caseSwitch) //each case should be a target station. If it meets the wrong one, move on.
        {
            case 0: //magFront

                if (magFrontConv == true && goStop == false && order == true)
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
                if (manualConv == true && goStop == false && order == true)
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
                if (camInspecConv == true && goStop == false && order == true)
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
                if (codesys1Conv == true && goStop == true && order == true)
                {
                    if (percentLapFirstIsland >= 37) { caseSwitch = 41; }
                    
                }
                break;

            case 41:
                pathMode = 2;
                caseSwitch = 50;
                break;

            case 50:
                codesys1OutConv = true; //THIS IS A BODGEEEEEEEEEEEEEEEEEEEEE REMOVEEEEEEEEEEEEEEEEE
                goStop = false; //AND THIS
                if (codesys1OutConv == true && goStop == false && order == true)
                {
                    busy = true;
                    caseSwitch = 55;
                }

                break;

            case 55:
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoIslandSensor == true)
                {
                    busy = false;
                    caseSwitch = 60;
                }
                break;

            case 60:
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true && order == true)
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
                goStop = true; // THIS IS ALSO A BODGEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE

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

                if (currentLocation == 14 && GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true)
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
                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true)
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
                if (toRobotPercentLap >= 100)
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

    
    void conveyors()
    {
        if (magFrontConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning == false) //I am on the conveyor
        {
            goStop = false;
            convStartMagFront = false;
        }

        if (magFrontConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning == true && convStartMagFront == false) //I am on the conveyor
        {
            goStop = true;
            convStartMagFront = true;
        }

        if (manualConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning == false) //I am on the conveyor
        {
            goStop = false;
            convStartManual = false;
        }

        if (manualConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning == true && convStartManual == false) //I am on the conveyor
        {
            goStop = true;
            convStartManual = true;
        }

        if (camInspecConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspecConvRunning == false) //I am on the conveyor
        {
            goStop = false;
            convStartCamInspec = false;
        }

        if (camInspecConv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspecConvRunning == true && convStartCamInspec == false) //I am on the conveyor
        {
            goStop = true;
            convStartCamInspec = true;
        }


        if (codesys1Conv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ConvRunning == false) //I am on the conveyor
        {
            goStop = false;
            convStartCodesys1 = false;
        }

        if (codesys1Conv == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1ConvRunning == true && convStartCodesys1 == false) //I am on the conveyor
        {
            goStop = true;
            convStartCodesys1 = true;
        }
    }

}


