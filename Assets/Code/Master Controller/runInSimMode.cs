using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    public bool pressStartInduction2 = false;
    public bool pressStopInduction2 = false;
    public bool pressEndInduction2 = false;
    public bool heatingStartInduction2 = false;
    public bool heatingStopInduction2 = false;
    public bool heatingEndInduction2 = false;
    public bool codesys2StopInduction2 = false;
    public bool codesys2ToRobotino2 = false;
    public bool codesys2FromRobotino2 = false;

    public GameObject carrierPrefab;
    public GameObject[] carriers;
    int[] carrierArray = { 0, 0, 0, 0, 0 };


    public int ID; //delete this after.

    // Start is called before the first frame update
    void Start()
    {
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
        carrierArray[ID] = ID;
        //carriers[ID].SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
     
       Prog();
    }

    void Prog()
    {
        switch (caseSwitch)
        {
            case 0:

                

                break;
        }
    }
}
