using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarrierMove : MonoBehaviour
{
    public float x = 1000f;
    public float y = 1000f;
    public float z = 1000f;
    public int carrierID = 0;
    public int initPos = 0;
    float speed = 0.005f;
    bool initialise = false;
    public int startMan = 0;
    public int middleMan = 0;
    public int relMan = 0;
    public int endMan = 0;
    public string locationData;
    public string currentLocation;

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

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(x, y, z);
        
    }

    // Update is called once per frame
    void Update()
    {
        sortData();
        initCarrierPos();
        transform.position = new Vector3(x, y, z);
        track();
    }




    void initCarrierPos()
    {
        if (initPos == 2 && initialise == false)
        {
            x = -6.6214f;
            y = 0.979f;
            z = 0.0533f;
            transform.position = new Vector3(x, y, z);
            initialise = true;
        }
    }


    void track()
    {
        
        if (transform.position.y == 0.979f && transform.position.x == -6.6214f && transform.position.z > -0.1889f && transform.position.z < 0.446f)
        {
            z -= speed;
        }
    }

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
