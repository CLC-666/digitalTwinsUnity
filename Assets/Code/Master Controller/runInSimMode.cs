using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;



public class runInSimMode : MonoBehaviour
{
    public int caseSwitch = 0;
    public float speed = 25f;
    public bool simMode = false;

    public bool manualStartInduction = false;
    public bool manualStopInduction = false; //SPAWN LOCATION 2 = follower: 92%.
    public bool manualEndInduction = false;
    public int manualRFID = 0;
    public int manualConveyorSpeed = 0;
    public bool manualCarrierRelease = false;
    public bool camInspectStartInduction = false;
    public bool camInspectStopInduction = false; //SPAWN LOCATION 3 = follower: 17%.
    public bool camInspectEndInduction = false;
    public bool camInspectCarrierRelease = false;
    public int camInspectRFID = 0;
    public int camInspectConveyorSpeed = 0;
    public bool codesys1StartInduction = false;
    public bool codesys1StopInduction = false; //SPAWN LOCATION 4 = follower: 36.5%.
    public bool codesys1ToRobotino = false;
    public bool codesys1FromRobotino = false;
    public bool codesys1EndInduction = false;
    public bool codesys1CarrierRelease = false;
    public int codesys1RFID = 0;
    public bool robotinoIslandSensor = false;
    public bool robotinoCarrierStop = false;
    public bool firstIslandRobotino = false;
    public bool secondIslandRobotino = false;
    public bool magFrontStartInduction = false;
    public bool magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
    public bool magFrontEndInduction = false;
    public bool magFrontCarrierRelease = false;
    public bool magFrontTop = false;
    public bool magFrontBottom = false;
    public int magFrontRFID = 0;
    public int magFrontConveyorSpeed = 0;
    public bool magBackStartInduction2 = false;
    public bool magBackStopInduction2 = false;
    public bool magBackEndInduction2 = false;
    public bool magBackCarrierRelease2 = false;
    public bool magBackTop = false;
    public bool magBackBottom = false;
    public int magBackRFID2 = 0;
    public int magBackConveyorSpeed2 = 0;
    public bool pressStartInduction2 = false;
    public bool pressStopInduction2 = false;
    public bool pressEndInduction2 = false;
    public bool pressCarrierRelease2 = false;
    public float pressTargetPressureN = 70;
    public float pressCurrentPressureN = 0;
    public float pressCurrentTime = 2;
    public float pressTargetTime;
    public bool pressWorkpieceCheck = false;
    public int pressRFID2 = 0;
    public int pressConveyorSpeed2 = 0;
    public bool heatingStartInduction2 = false;
    public bool heatingStopInduction2 = false;
    public bool heatingEndInduction2 = false;
    public bool heatingCarrierRelease2 = false;
    public float heatingTargetTime;
    public float heatingCurrentTime;
    public float heatingTargetTemp;
    public float heatingCurrentTemp = 0;
    public int heatingRFID2 = 0;
    public int heatingConveyorSpeed2 = 0;
    public bool codesys2StartInduction2 = false;
    public bool codesys2StopInduction2 = false;
    public bool codesys2ToRobotino2 = false;
    public bool codesys2FromRobotino2 = false;
    public bool codesys2CarrierRelease2 = false;
    public bool codesys2EndInduction2 = false;
    public int codesys2RFID2 = 0;

    public bool magFrontConvRunning = true;
    public bool manualConvRunning = true;
    public bool camInspectConvRunning = true;
    public bool codesys1ConvRunning = true;

    bool manualSpawnRunOnce = false;


    public GameObject carrierPrefab;
    public GameObject[] carriers;
    int[] carrierArray = { 0, 1, 2, 3, 4 };

    public List<string> sensorNames = new List<string>();
    public List<object> sensorValues = new List<object>();
    public bool startRecording = false;
    string filePath;
    string toWrite;
    StreamWriter writer;
    int counter;
    int updateCounter;
    int dataLogCounter = 0;



    public int ID; //delete this after.

    // Start is called before the first frame update
    void Start()
    {
        sensorNames = new List<string>() {
            "manualStartInduction", "manualStopInduction", "manualEndInduction", "camInspectStartInduction",
            "camInspectStopInduction", "camInspectEndInduction", "codesys1StopInduction", "codesys1ToRobotino",
            "codesys1FromRobotino", "robotinoIslandSensor", "robotinoCarrierStop", "firstIslandRobotino",
            "secondIslandRobotino", "magFrontStartInduction", "magFrontStopInduction", "magFrontEndInduction",
            "magFrontTop", "magFrontBottom", "magBackStartInduction2", "magBackStopInduction2", "magBackEndInduction2",
            "magBackTop", "magBackBottom", "pressStartInduction2", "pressStopInduction2", "pressEndInduction2",
            "pressTargetPressureN", "pressCurrentPressureN", "pressTargetTime", "pressCurrentTime", "pressWorkpieceCheck", "heatingStartInduction2", "heatingStopInduction2",
            "heatingEndInduction2", "heatingCurrentTime", "heatingTargetTime", "heatingCurrentTemp", "heatingTargetTemp", "codesys2StopInduction2",
            "codesys2ToRobotino2", "codesys2FromRobotino2"
        };

        
        sensorInitialisations();

        carriers = new GameObject[5];

        if (simMode == true)
        {
            GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().simMode = true;
            // Run in sim mode
            for (int i = 1; i < 2; i++)
            {
                ID = i; //just for testing purposes.
                Debug.Log(carrierArray[ID]);
                Debug.Log("creating a carrier");
                GameObject clone = Instantiate(carrierPrefab) as GameObject;
                carriers[ID] = clone;
                carriers[ID].GetComponent<follower>().carrierID = ID;
                carriers[ID].GetComponent<follower>().spawnLocation = i;
                carriers[ID].GetComponent<follower>().order = false;
                carriers[ID].GetComponent<follower>().goStop = true;
                carriers[ID].GetComponent<follower>().speed = speed;
                carriers[ID].name = carriers[ID].name + " " + i.ToString();
                carrierArray[ID] = ID;

            }
        }



        if (simMode == false)
        {
            GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().simMode = false;
        }
            
        


    }

    public void Update()
    {
        if (simMode == false)
        {
            //station name, start induct, stop induct, carrier released, end induct, carrier number
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().manData);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().magFData);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().camData);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().codesys1Data);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().codesys2Data);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().magBData);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().pressData);
            dataDist(GameObject.Find("Main Camera").GetComponent<MultiThreadServer170520>().heatData);
        }

        if (manualCarrierRelease == true && manualSpawnRunOnce == false && carrierArray[ID] != manualRFID)
        {
            ID = manualRFID;
            GameObject clone = Instantiate(carrierPrefab) as GameObject;
            carriers[ID] = clone;
            carriers[ID].GetComponent<follower>().carrierID = ID;
            carriers[ID].GetComponent<follower>().spawnLocation = 2;
            carriers[ID].GetComponent<follower>().order = false;
            carriers[ID].GetComponent<follower>().goStop = true;
            carriers[ID].GetComponent<follower>().speed = 4.5f;
            carriers[ID].GetComponent<follower>().pauseTime = 5000;
            carriers[ID].GetComponent<follower>().manualSpawnInit = true;
            carriers[ID].name = carriers[ID].name + ID.ToString();
            carrierArray[ID] = ID;
            manualSpawnRunOnce = true;
        }

        if (manualCarrierRelease == false && manualSpawnRunOnce == true)
        {
            manualSpawnRunOnce = false;
        }
    }

    public void dataDist(string data)
    {

        string[] splitData = data.Split(',');
        //Debug.Log(splitData[0] + " splitdata");
        try
        {

            if (splitData[0].Equals("1"))
            {
                magFrontStartInduction = splitData[1].Equals("1");
                magFrontStopInduction = splitData[2].Equals("1");
                magFrontCarrierRelease = splitData[3].Equals("1");
                magFrontEndInduction = splitData[4].Equals("1");
                magFrontRFID = Int32.Parse(splitData[5]);
                magFrontConveyorSpeed = Int32.Parse(splitData[6]);

            }

            if (splitData[0].Equals("2"))
            {
                manualStartInduction = splitData[1].Equals("1");
                manualStopInduction = splitData[2].Equals("1");
                manualCarrierRelease = splitData[3].Equals("1");
                manualEndInduction = splitData[4].Equals("1");
                manualRFID = Int32.Parse(splitData[5]);
                manualConveyorSpeed = Int32.Parse(splitData[6]);
            }

            if (splitData[0].Equals("3"))
            {
                camInspectStartInduction = splitData[1].Equals("1");
                camInspectStopInduction = splitData[2].Equals("1");
                camInspectCarrierRelease = splitData[3].Equals("1");
                camInspectEndInduction = splitData[4].Equals("1");
                camInspectRFID = Int32.Parse(splitData[5]);
                camInspectConveyorSpeed = Int32.Parse(splitData[6]);
            }

            if (splitData[0].Equals("4"))
            {
                codesys1StartInduction = splitData[1].Equals("1");
                codesys1StopInduction = splitData[2].Equals("1");
                codesys1CarrierRelease = splitData[3].Equals("1");
                codesys1EndInduction = splitData[4].Equals("1");
                codesys1RFID = Int32.Parse(splitData[5]);
            }

            if (splitData[0].Equals("5"))
            {
                codesys2StartInduction2 = splitData[1].Equals("1");
                codesys2StopInduction2 = splitData[2].Equals("1");
                codesys2CarrierRelease2 = splitData[3].Equals("1");
                codesys2EndInduction2 = splitData[4].Equals("1");
                codesys2RFID2 = Int32.Parse(splitData[5]);
            }

            if (splitData[0].Equals("6"))
            {
                magBackStartInduction2 = splitData[1].Equals("1");
                magBackStopInduction2 = splitData[2].Equals("1");
                magBackCarrierRelease2 = splitData[3].Equals("1");
                magBackEndInduction2 = splitData[4].Equals("1");
                magBackRFID2 = Int32.Parse(splitData[5]);
                magBackConveyorSpeed2 = Int32.Parse(splitData[6]);
            }

            if (splitData[0].Equals("7"))
            {
                pressStartInduction2 = splitData[1].Equals("1");
                pressStopInduction2 = splitData[2].Equals("1");
                pressCarrierRelease2 = splitData[3].Equals("1");
                pressEndInduction2 = splitData[4].Equals("1");
                pressRFID2 = Int32.Parse(splitData[5]);
                pressConveyorSpeed2 = Int32.Parse(splitData[6]);
            }

            if (splitData[0].Equals("8"))
            {
                heatingStartInduction2 = splitData[1].Equals("1");
                heatingStopInduction2 = splitData[2].Equals("1");
                heatingCarrierRelease2 = splitData[3].Equals("1");
                heatingEndInduction2 = splitData[4].Equals("1");
                heatingRFID2 = Int32.Parse(splitData[5]);
                heatingConveyorSpeed2 = Int32.Parse(splitData[6]);
            }
        }
        catch (Exception e) { }

    }


    



    void FixedUpdate()
    {
       

           // writeToCSV();
        

    }

    void writeToCSV()
    {
        sensorValues = new List<object>(){
            manualStartInduction, manualStopInduction, manualEndInduction, camInspectStartInduction,
            camInspectStopInduction, camInspectEndInduction, codesys1StopInduction, codesys1ToRobotino,
            codesys1FromRobotino, robotinoIslandSensor, robotinoCarrierStop, firstIslandRobotino,
            secondIslandRobotino, magFrontStartInduction, magFrontStopInduction, magFrontEndInduction,
            magFrontTop, magFrontBottom, magBackStartInduction2, magBackStopInduction2, magBackEndInduction2,
            magBackTop, magBackBottom, pressStartInduction2, pressStopInduction2, pressEndInduction2,
            pressTargetPressureN, pressCurrentPressureN, pressTargetTime, pressCurrentTime, pressWorkpieceCheck, heatingStartInduction2, heatingStopInduction2,
            heatingEndInduction2, heatingCurrentTime, heatingTargetTime, heatingCurrentTemp, heatingTargetTemp, codesys2StopInduction2,
            codesys2ToRobotino2, codesys2FromRobotino2
        };

        if (startRecording == false && GameObject.Find("carrier without workpiece(Clone)").GetComponent<follower>().order == true)
        {
            filePath = getPath();
            writer = new StreamWriter(filePath);
            writer.WriteLine("Time,Sensor_and_Value");
            startRecording = true;
        }

        if (updateCounter < sensorNames.Count && startRecording == true)
        {
            toWrite += sensorNames[updateCounter] + " = " + sensorValues[updateCounter] + "|";
            updateCounter++;
        }

        if (updateCounter == sensorNames.Count && startRecording == true)
        {
            writer.WriteLine(Time.time + "," + toWrite);


            writer.Flush();
            if (GameObject.Find("carrier without workpiece(Clone)").GetComponent<follower>().order == false)
            {
                writer.Close();
                dataLogCounter++;
                startRecording = false;
                updateCounter = 0;
            }

            updateCounter = 0;
            toWrite = "";
        }
}

    private string getPath()
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/Data/" + "Saved_Inventory" + dataLogCounter + ".csv";
        //"Participant " + "   " + DateTime.Now.ToString("dd-MM-yy   hh-mm-ss") + ".csv";
        #elif UNITY_ANDROID
            return Application.persistentDataPath+"Saved_Inventory.csv";
        #elif UNITY_IPHONE
            return Application.persistentDataPath+"/"+"Saved_Inventory.csv";
        #else
            return Application.dataPath +"/"+"Saved_Inventory.csv";
        #endif
    }


    void sensorInitialisations()
    {
        manualStartInduction = false;
        manualStopInduction = false; //SPAWN LOCATION 2 = follower: 92%.
        manualEndInduction = false;
        camInspectStartInduction = false;
        camInspectStopInduction = false; //SPAWN LOCATION 3 = follower: 17%.
        camInspectEndInduction = false;
        codesys1StopInduction = false; //SPAWN LOCATION 4 = follower: 36.5%.
        codesys1ToRobotino = false;
        codesys1FromRobotino = false;
        robotinoIslandSensor = false;
        robotinoCarrierStop = false;
        firstIslandRobotino = false;
        secondIslandRobotino = false;
        magFrontStartInduction = false;
        magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
        magFrontEndInduction = false;
        magFrontTop = false;
        magFrontBottom = false;
        magBackStartInduction2 = false;
        magBackStopInduction2 = false;
        magBackEndInduction2 = false;
        magBackTop = false;
        magBackBottom = false;
        pressStartInduction2 = false;
        pressStopInduction2 = false;
        pressEndInduction2 = false;
        pressTargetPressureN = 70;
        pressCurrentPressureN = 0;
        pressTargetTime = 2;
        pressCurrentTime = 0;
        pressWorkpieceCheck = false;
        heatingStartInduction2 = false;
        heatingStopInduction2 = false;
        heatingEndInduction2 = false;
        heatingTargetTime = 5;
        heatingTargetTemp = 30;
        heatingCurrentTemp = 0;
        codesys2StopInduction2 = false;
        codesys2ToRobotino2 = false;
        codesys2FromRobotino2 = false;
}



}
