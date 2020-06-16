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
    public float pressPressureN = 0;
    public float pressPressTime = 2;
    public bool pressWorkpieceCheck = false;
    public bool heatingStartInduction2 = false;
    public bool heatingStopInduction2 = false;
    public bool heatingEndInduction2 = false;
    public float heatingHeatTime;
    public float heatingHeatTemp;
    public float heatingCurrentTemp = 0;
    public bool codesys2StopInduction2 = false;
    public bool codesys2ToRobotino2 = false;
    public bool codesys2FromRobotino2 = false;

    public GameObject carrierPrefab;
    public GameObject[] carriers;
    int[] carrierArray = { 0, 0, 0, 0, 0 };

    public List<string> sensorNames = new List<string>();
    public List<string> values = new List<string>();
    public bool startRecording = false;
    string filePath;
    StreamWriter writer;


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
            "pressPressureN", "pressPressTime", "pressWorkpieceCheck", "heatingStartInduction2", "heatingStopInduction2",
            "heatingEndInduction2", "heatingHeatTime", "heatingHeatTemp", "heatingCurrentTemp", "codesys2StopInduction2",
            "codesys2ToRobotino2", "codesys2FromRobotino2"
        };

        sensorInitialisations();

        carriers = new GameObject[5];


        //if (carrierArray[manual["ID"]] == 0 && manual["rel"] == 1)
        //{
        //int ID = manual["ID"];
        ID = 2; //just for testing purposes.
        Debug.Log(carrierArray[ID]);
        Debug.Log("creating a carrier");
        GameObject clone = Instantiate(carrierPrefab) as GameObject;
        carriers[ID] = clone;
        carriers[ID].GetComponent<follower>().carrierID = ID;
        carriers[ID].GetComponent<follower>().spawnLocation = 4;
        carriers[ID].GetComponent<follower>().caseSwitch = 0;
        carriers[ID].GetComponent<follower>().currentLocation = 0;
        carrierArray[ID] = ID;
        //carriers[ID].SetActive(false);
        //}
        
    }

    void Update()
    {
        writeToCSV();
    }

    void writeToCSV()
    {
        if (startRecording == false)
        {
            filePath = getPath();
            writer = new StreamWriter(filePath);
            writer.WriteLine("Time,Sensor_Name,Value");
            startRecording = true;
        }

        writer.WriteLine(Time.time + ",");


        writer.Flush();
        writer.Close();
    }

    private string getPath()
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/Data/" + "Saved_Inventory.csv";
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
        pressPressureN = 0;
        pressPressTime = 2;
        pressWorkpieceCheck = false;
        heatingStartInduction2 = false;
        heatingStopInduction2 = false;
        heatingEndInduction2 = false;
        heatingHeatTime = 5;
        heatingHeatTemp = 30;
        heatingCurrentTemp = 0;
        codesys2StopInduction2 = false;
        codesys2ToRobotino2 = false;
        codesys2FromRobotino2 = false;
}
}
