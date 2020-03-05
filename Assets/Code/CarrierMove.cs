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
    float speedConst = 0.0035f;
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
    float angleSpeed = 0;
    bool initcase40 = false;
    int counter = 0;
    bool pause = false;
    bool case75init = false;
    

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
        //initCarrierPos();
        transform.position = new Vector3(x, y, z);
        track();
        sensorDefinitions();
    }




    //void initCarrierPos()
    //{
    //    //if (initPos == 2 && initialise == false)
    //    if (1 == 1 && initialise == false)
    //    {
    //        x = -6.6214f;
    //        y = 0.979f;
    //        z = 0.0533f;
    //        transform.position = new Vector3(x, y, z);
    //        initialise = true;
    //    }
    //}


    void track()
    {
        //Debug.Log(caseSwitch);

        

        switch (caseSwitch)
        {


            case 30:

                //if (transform.position.y == 0.979f && transform.position.x == -6.6214f && transform.position.z > -0.1959f && transform.position.z < 0.446f && manual["rel"] == 1)
                //Debug.Log(manual["rel"]);
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
                    counter = 0;
                    angleSpeed = 0.2f;
                    caseSwitch = 40;
                }
                break;

            case 40: //start x = -6.6214, z =  -0.1959 | end x = -6.372, z = -0.4502 | difference x = 0.2494, z = -0.2543
                if (pause == false)
                {

                    angle = transform.eulerAngles.y * (Convert.ToSingle(Math.PI) / 180);
                    //Debug.Log(angle);
                    x = -6.3741f + 0.27f * Convert.ToSingle(Math.Cos(-angle + (Math.PI / 2)));
                    z = -0.21f + 0.27f * Convert.ToSingle(Math.Sin(-angle + (Math.PI / 2)));

                    Quaternion target = Quaternion.Euler(0, 170, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * angleSpeed);
                }

                if (transform.eulerAngles.y > 210 && transform.eulerAngles.y < 212 && camInspec["first"] == 0)
                {
                    pause = true;
                }

                if (camInspec["first"] == 1)
                {
                    pause = false;
                    angleSpeed = 0.9f;
                }

                if (transform.eulerAngles.y > 190 && transform.eulerAngles.y < 192)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    z = -0.4502f;
                    caseSwitch = 43;
                }


                break;
            case 43:

                x += speed;

                if (x < (-6.1049 + (speed / 2)) && x > (-6.1049 - (speed / 2)))
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
                    angleSpeed = 0.2f;
                    pause = false;
                    caseSwitch = 55;
                }

                //if (x < (-5.876f + (speed / 2)) && x > (-5.876f - (speed / 2)))
                //{
                //    x = -5.876f;

                //    angleSpeed = 0.2f;
                //    pause = false;
                //    caseSwitch = 55;
                //}

                break;

            case 55:
                if (pause == false)
                {

                    angle = transform.eulerAngles.y * (Convert.ToSingle(Math.PI) / 180);
                    //Debug.Log(transform.eulerAngles.y);
                    x = -5.876f + 0.27f * Convert.ToSingle(Math.Cos(-angle + (Math.PI / 2)));
                    z = -0.21f + 0.27f * Convert.ToSingle(Math.Sin(-angle + (Math.PI / 2)));

                    Quaternion target2 = Quaternion.Euler(0, 80, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime * angleSpeed);
                }

                if (transform.eulerAngles.y > 120 && transform.eulerAngles.y < 122 && cpBridge["first"] == 0)
                {
                    pause = true;
                }

                if (cpBridge["first"] == 1)
                {
                    pause = false;
                    angleSpeed = 0.9f;
                }

                if (transform.eulerAngles.y > 90 && transform.eulerAngles.y < 92)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    caseSwitch = 57;
                }



                break;

            case 57:
                z += speed;

                if (cpBridge["middle"] == 1)
                {
                    z = -0.148f;
                    caseSwitch = 60;
                }
                break;

            case 60:
                if (cpBridge["rel"] == 1)
                {
                    caseSwitch = 63;
                }
                break;

            case 63:
                z += speed;

                if (cpBridge["end"] == 1)
                {
                    z = 0.335f;
                    angleSpeed = 0.2f;
                    pause = false;
                    caseSwitch = 65;
                }

                break;

            case 65:
                if (pause == false)
                {
                    angle = transform.eulerAngles.y * (Convert.ToSingle(Math.PI) / 180);
                    //Debug.Log(transform.eulerAngles.y);
                    x = -5.88f + 0.27f * Convert.ToSingle(Math.Cos((Math.PI / 2) - angle));
                    z = 0.31f + 0.27f * Convert.ToSingle(Math.Sin((Math.PI / 2) - angle));

                    Quaternion target3 = Quaternion.Euler(0, -10, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target3, Time.deltaTime * angleSpeed);
                }

                if (transform.eulerAngles.y > 25 && transform.eulerAngles.y < 27 && magFront["first"] == 0)
                {
                    pause = true;
                }

                if (magFront["first"] == 1)
                {
                    pause = false;
                    angleSpeed = 0.9f;
                }

                if (transform.eulerAngles.y > 0 && transform.eulerAngles.y < 2)
                {
                    z = 0.5573f;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    caseSwitch = 67;
                }

                break;

            case 67:
                x -= speed;

                if (magFront["middle"] == 1)
                {
                    x = -6.127f;
                    caseSwitch = 70;
                }
                break;

            case 70:
                

                if (magFront["rel"] == 1)
                {
                    caseSwitch = 73;
                }
                break;

            case 73:

                x -= speed;

                if (magFront["end"] == 1)
                {
                    x = -6.375f;
                    angleSpeed = 0.2f;
                    pause = false;
                    caseSwitch = 75;
                }

                break;

            case 75:

                if (pause == false)
                {
                    Debug.Log(transform.eulerAngles.y);

                    angle = transform.eulerAngles.y * (Convert.ToSingle(Math.PI) / 180);
                    //Debug.Log(transform.eulerAngles.y);
                    x = -6.375f + 0.27f * Convert.ToSingle(Math.Cos(-angle - 3 * (Math.PI / 2)));
                    z = 0.317f + 0.27f * Convert.ToSingle(Math.Sin(-angle - 3 *(Math.PI / 2)));

                    Quaternion target4 = Quaternion.Euler(0, 260, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target4, Time.deltaTime * angleSpeed);
                }

                if (transform.eulerAngles.y > 297 && transform.eulerAngles.y < 299 && manual["first"] == 0)
                {
                    pause = true;
                }

                if (manual["first"] == 1)
                {
                    pause = false;
                    angleSpeed = 0.9f;
                }

                if (transform.eulerAngles.y > 270 && transform.eulerAngles.y < 272)
                {
                    x = -6.6214f;
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    caseSwitch = 77;
                }

                break;

            case 77:
                z -= speed;

                if (manual["middle"] == 1)
                {
                    x = -6.6214f;
                    z = 0.0533f;
                    caseSwitch = 30;
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
            magFront["liftisUp"] = Int16.Parse(sensors[6]);
            magFront["liftisDown"] = Int16.Parse(sensors[7]);
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
            cpBridge["middle"] = Int16.Parse(sensors[2]);
            cpBridge["rel"] = Int16.Parse(sensors[3]);
            cpBridge["end"] = Int16.Parse(sensors[4]);
            cpBridge["ID"] = Int16.Parse(sensors[5]);

        }
    }


}