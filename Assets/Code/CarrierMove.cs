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
    float speedConst = 0.0039f;
    float speed = 0;
    bool initialise = false;
    public int startMan = 0;
    public int middleMan = 0;
    public int relMan = 0;
    public int endMan = 0;
    public string locationData;
    public string currentLocation;
    public int caseSwitch = 0;
    float angle = 3.141592f; //4.71239
    public float angleSpeed = 0;
    bool initcase40 = false;
    

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
    public Dictionary<string, int> cpBridge = new Dictionary<string, int>()
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
       speed = speedConst;
    }

    // Update is called once per frame
    void Update()
    {
        sortData();
        initCarrierPos();
        transform.position = new Vector3(x, y, z);
        track();
        sensorDefinitions();
    }




    void initCarrierPos()
    {
        //if (initPos == 2 && initialise == false)
        if (1 == 1 && initialise == false)
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


        //Debug.Log("case: " + caseSwitch);
        //xsin(angle) = x | zsin(angle) = z
        switch (caseSwitch)
        {
            case 0:

                if (initPos == 2)
                {
                    caseSwitch = 30;
                }
                break;

            case 30:

                //if (transform.position.y == 0.979f && transform.position.x == -6.6214f && transform.position.z > -0.1959f && transform.position.z < 0.446f && manual["rel"] == 1)
                Debug.Log(manual["rel"]);
                if (manual["rel"] == 1)
                {
                    caseSwitch = 35;
                }
                break;

            case 35:
               
                if (transform.position.y == 0.979f && transform.position.x == -6.6214f && transform.position.z > -0.1959f && transform.position.z < 0.446f)
                {
                    z -= speed;
                }

                if (manual["end"] == 1)
                {
                    //z = -0.21f; //-0.1959 -0.2051
                    caseSwitch = 40;
                }
                break;

            case 40: //start x = -6.6214, z =  -0.1959 | end x = -6.372, z = -0.4502 | difference x = 0.2494, z = -0.2543
                x = -6.3741f + 0.27f * Convert.ToSingle(Math.Cos(angle));
                z = -0.21f + 0.25f * Convert.ToSingle(Math.Sin(angle));
                Quaternion target = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 0.7f);
                if (angle <= 4.71239)
                {
                    angle += Convert.ToSingle(0.007);
                }
                
                if (x <= -6.46 && x >= -6.47 )
                {
                    
                    //x = -6.372f;
                    z = -0.4502f;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    initcase40 = true;
                }



                //if (angle <= 3.1416f)
                //{
                //    x = -6.372f;
                //    z = -0.4502f;
                //    transform.eulerAngles = new Vector3(0,180,0);
                //    caseSwitch = 43;
                //}
                //0.666723259762309 0.0416702037351443
                //x += speed;
                //z -= speed;

                if (camInspec["first"] == 1 && initcase40)
                {
                    caseSwitch = 43;
                    

                }


                //Quaternion target = Quaternion.Euler(0, 180, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
                break;
            case 43:

                x += speed;

                if (x < (-6.1049 + (speed/2)) && x > (-6.1049 - (speed / 2)))
                {
                    caseSwitch = 45;
                }
                break;

                if (camInspec["middle"] == 1)
                {
                    x = -6.1049f;
                    caseSwitch = 45;
                }

            case 45:

                if (camInspec["rel"] == 1)
                {
                    caseSwitch = 50;
                }

                break;

            case 50:

                x += speed;

                if (camInspec["end"] == 1)
                {
                    x = -5.876f;
                    caseSwitch = 55;
                }

                if (x < (-5.876f + (speed / 2)) && x > (-5.876f - (speed / 2)))
                {
                    x = -5.876f;
                    angle = 4.71239f;
                    caseSwitch = 55;
                }

                break;

            case 55:
                // start x = -5.876 and final z = -0.213
                Debug.Log(angle);
                x = -5.876f + 0.27f * Convert.ToSingle(Math.Cos(angle));
                z = -0.21f + 0.25f * Convert.ToSingle(Math.Sin(angle));
                Quaternion target2 = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime * 0.7f);
                if (angle <= 6.28319f)
                {
                    angle += Convert.ToSingle(0.007);
                }



                //if (x <= -6.46 && x >= -6.47)
                //{

                //    //x = -6.372f;
                //    z = -0.4502f;
                //    transform.eulerAngles = new Vector3(0, 180, 0);
                //    initcase40 = true;
                //}
                //x = -5.876f + 0.263f * Convert.ToSingle(Math.Sin(angle));
                //z = -0.213f + 0.237f * Convert.ToSingle(Math.Cos(angle));
                //Quaternion target2 = Quaternion.Euler(0, 90, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime - (Time.deltaTime / 4));
                //if (angle > (3.1416f / 2))
                //{
                //    angle -= Convert.ToSingle(0.025);


                //}

                //if (angle <= (3.1416f/2))
                //{
                //    transform.eulerAngles = new Vector3(0, 90, 0);
                //    caseSwitch = 57;
                //}

                break;

            case 57:
                z += speed;

                if (cpBridge["start"] == 1)
                {
                    caseSwitch = 60;
                }
                break;
        }
    }

    void sensorDefinitions()
    {

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

        if (stationID == 7) //CP Bridge
        {
            cpBridge["first"] = Int16.Parse(sensors[1]);
            cpBridge["rel"] = Int16.Parse(sensors[2]);
        }
    }


}