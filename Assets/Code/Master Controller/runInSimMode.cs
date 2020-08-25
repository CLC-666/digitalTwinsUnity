using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;



public class runInSimMode : MonoBehaviour
{
    public int caseSwitch = 0;
    public float speed = 0.25f;

    public bool manualStartInduction = false;
    public bool manualStopInduction = false; //SPAWN LOCATION 2 = follower: 92%.
    public bool manualEndInduction = false;
    public bool camInspectStartInduction = false;
    public bool camInspectStopInduction = false; //SPAWN LOCATION 3 = follower: 17%.
    public bool camInspectEndInduction = false;
    public bool codesys1StopInduction = false; //SPAWN LOCATION 4 = follower: 36.5%.
    public bool codesys1ToRobotino = false;
    public bool codesys1FromRobotino = false;
    public bool robotinoIslandSensor = false;
    public bool robotinoCarrierStop = false;
    public bool firstIslandRobotino = false;
    public bool secondIslandRobotino = false;
    public bool magFrontStartInduction = false;
    public bool magFrontStopInduction = false; //SPAWN LOCATION 1 = follower: 67%.
    public bool magFrontEndInduction = false;
    public bool magFrontTop = false;
    public bool magFrontBottom = false;
    public bool magBackStartInduction2 = false;
    public bool magBackStopInduction2 = false;
    public bool magBackEndInduction2 = false;
    public bool magBackTop = false;
    public bool magBackBottom = false;
    public bool pressStartInduction2 = false;
    public bool pressStopInduction2 = false;
    public bool pressEndInduction2 = false;
    public float pressTargetPressureN = 70;
    public float pressCurrentPressureN = 0;
    public float pressCurrentTime = 2;
    public float pressTargetTime;
    public bool pressWorkpieceCheck = false;
    public bool heatingStartInduction2 = false;
    public bool heatingStopInduction2 = false;
    public bool heatingEndInduction2 = false;
    public float heatingTargetTime;
    public float heatingCurrentTime;
    public float heatingTargetTemp;
    public float heatingCurrentTemp = 0;
    public bool codesys2StopInduction2 = false;
    public bool codesys2ToRobotino2 = false;
    public bool codesys2FromRobotino2 = false;
    

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

        // Run in sim mode
        for (int i = 1; i < 3; i++)
        {
            ID = i; //just for testing purposes.
            Debug.Log(carrierArray[ID]);
            Debug.Log("creating a carrier");
            GameObject clone = Instantiate(carrierPrefab) as GameObject;
            carriers[ID] = clone;
            carriers[ID].GetComponent<follower>().carrierID = ID;
            carriers[ID].GetComponent<follower>().spawnLocation = i;
            carriers[ID].GetComponent<follower>().simMode = true;
            carriers[ID].GetComponent<follower>().order = true;
            carrierArray[ID] = ID;
        }

        //carriers[ID].SetActive(false);
        //}

        //if (carrierArray[manual["ID"]] == 0 && manual["rel"] == 1)
        //{
        //    int ID = manual["ID"];
        //    Debug.Log(carrierArray[ID]);
        //    Debug.Log("creating a carrier");
        //    GameObject clone = Instantiate(carrierPrefab) as GameObject;
        //    carriers[ID] = clone;
        //    carriers[ID].GetComponent<CarrierMove>().carrierID = ID;

        //    //carriers[ID].GetComponent<CarrierMove>().x = -6.6214f;
        //    //carriers[ID].GetComponent<CarrierMove>().y = 0.979f;
        //    //carriers[ID].GetComponent<CarrierMove>().z = 0.0533f;
        //    //carriers[ID].GetComponent<CarrierMove>().caseSwitch = 35;
        //    carrierArray[ID] = ID;
        //    //carriers[ID].SetActive(false);

        //}

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
