using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class magFrontLiftScript : MonoBehaviour
{
    float y;
    float speed = 0.01f;
    bool move = false;
    string locationData;
    float oldPos;
    bool wasUp = false;
    bool wasDown = false;
    bool stop = false;
    public GameObject workpiecePrefab;
    public GameObject[] workpieces;

    public Dictionary<string, int> magFront = new Dictionary<string, int>()
        {
            {"first", 0},
            {"middle", 0},
            {"rel", 0},
            {"end",0},
            {"ID", 0 },
        {"liftisUp", 1 },
        {"liftisDown", 0 },
        {"nextResource", 0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        y = 0.956f;
        workpieces = new GameObject[5];
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-6.091f, y, 0.556f);

        locationData = GameObject.Find("firstIsland").GetComponent<ServerClient>().locationData;
        //Debug.Log(locationData);
        string[] sensors = locationData.Split(',');
        int stationID = Int16.Parse(sensors[0]);
        if (stationID == 1)
        {
            magFront["ID"] = Int16.Parse(sensors[5]);
            magFront["liftisUp"] = Int16.Parse(sensors[6]);
            magFront["liftisDown"] = Int16.Parse(sensors[7]);
            magFront["nextResource"] = Int16.Parse(sensors[8]);
        }
        //Debug.Log(magFront["nextResource"] + " " + magFront["liftisUp"]);

        //if (magFront["nextResource"] == 2)
        //{
        if (magFront["liftisUp"] == 1)
        {
            y = 0.956f;
            wasUp = true;
            wasDown = false;
            stop = false;
        }

        if (magFront["liftisDown"] == 1)
        {
            y = 0.9298f;
            wasUp = false;
            wasDown = true;
            //GameObject clone = Instantiate(workpiecePrefab) as GameObject;
            //workpieces[magFront["ID"]] = clone; 
            
            stop = false;
        }

        if (magFront["liftisUp"] == 0 && magFront["liftisDown"] == 0)
        {
            if (wasUp == true && stop == false)
            {
                y -= speed;
                if (y <= 0.9298f)
                {
                    stop = true;
                }
            }

            if (wasDown == true && stop == false)
            {
                y += speed;

                if (y >= 0.9452f)
                {
                    stop = true;
                }
            }
            //}


        }
        
    }
}
