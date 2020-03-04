using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnCarriage : MonoBehaviour
{
    public GameObject carrierPrefab;
    public GameObject[] carriers;
    public string locationData;


    public Dictionary<string, int> magFront = new Dictionary<string, int>()
        {
            {"first", 0},
            {"middle", 0},
            {"rel", 0},
            {"end",0},
            {"ID", 0 }
        };

    public Dictionary<string, int> manual = new Dictionary<string, int>()
        {
            {"first", 0},
            {"middle", 0},
            {"rel", 0},
            {"end",0},
            {"ID", 0 }
        };

    public Dictionary<string, int> camInspec = new Dictionary<string, int>()
        {
            {"first", 0},
            {"middle", 0},
            {"rel", 0},
            {"end",0},
            {"ID", 0 }
        };

    int[] carrierArray = { 0, 0, 0, 0, 0 };


    void Start()
    {

        carriers = new GameObject[5];
    }

    void Update()
    {
        sortData();

        if (manual["ID"] > 0 && carrierArray[manual["ID"]] == 0)
        {
            int ID = manual["ID"];
            Debug.Log(carrierArray[ID]);
            Debug.Log("creating a carrier");
            GameObject clone = Instantiate(carrierPrefab) as GameObject;
            carriers[ID] = clone;
            carriers[ID].GetComponent<CarrierMove>().carrierID = ID;
            carriers[ID].GetComponent<CarrierMove>().initPos = 2;
            carrierArray[ID] = ID;
        }





        //Debug.Log(magFront["first"]);
    }

    //private void spawnCarrier()
    //{
    //    GameObject a = Instantiate(carrierPrefab) as GameObject;
    //    a.GetComponent<CarrierMove>().carrierID = 1;

    //    a.transform.positoin = new Vector3(x, y, z);
    //}

    void sortData()
    {

        locationData = GameObject.Find("firstIsland").GetComponent<ServerClient>().locationData;

        string[] sensors = locationData.Split(',');
        int stationID = Int16.Parse(sensors[0]);
        int first = Int16.Parse(sensors[1]);
        if (stationID == 1)
        {
            //Debug.Log(locationData);
            magFront["first"] = Int16.Parse(sensors[1]);
            magFront["middle"] = Int16.Parse(sensors[2]);
            magFront["rel"] = Int16.Parse(sensors[3]);
            magFront["end"] = Int16.Parse(sensors[4]);
            magFront["ID"] = Int16.Parse(sensors[5]);
        }

        if (stationID == 2)
        {
            //Debug.Log(locationData);
            manual["first"] = Int16.Parse(sensors[1]);
            manual["middle"] = Int16.Parse(sensors[2]);
            manual["rel"] = Int16.Parse(sensors[3]);
            manual["end"] = Int16.Parse(sensors[4]);
            manual["ID"] = Int16.Parse(sensors[5]);
        }

        if (stationID == 3)
        {
            //Debug.Log(locationData);
            camInspec["first"] = Int16.Parse(sensors[1]);
            camInspec["middle"] = Int16.Parse(sensors[2]);
            camInspec["rel"] = Int16.Parse(sensors[3]);
            camInspec["end"] = Int16.Parse(sensors[4]);
            camInspec["ID"] = Int16.Parse(sensors[5]);
        }
    }





}