using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Networking;
using System.Linq;
using System;

public class robotinoScript : MonoBehaviour
{
    float startX = -4.964f;
    float startY = -0.014f;
    float startZ = 0.072f;
    public bool robotinoIslandSensor = false;
    public PathCreator robotinoPath;
    public float distanceTravelled;
    public float percentLap = 0;
    public bool goStop = false;
    public string caseSwitch;
    public bool turningPoint1 = false;
    public bool turningPoint2 = false;
    public float angle = 180;
    public float rotateSpeed = 1;
    public bool rotated = false;
    public bool rotating = false;
    public bool toIsland2 = false;
    public bool toIsland1 = false;
    public string robotinoOdo;
    public float zCalib = 1.8f;
    public float xCalib = -7.6f;
    public float rotationCalib = 0;

    float oldx = 0;
    float oldy = 0;
    float oldAngle = 0;
    float newAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        goStop = false;
        
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
            robotinoOdo = uwr.downloadHandler.text;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == false)
        //{
        //    StartCoroutine(GetRequest("172.21.9.90/data/odometry"));
        //    //// [x,y,rot,vx,vy,omega,seq] distance in meters
        //    string[] odoArray = robotinoOdo.Split(',');
        //    string xString = odoArray[0].ToString().Substring(6, 11);
        //    string yString = odoArray[1].ToString().Substring(6, 11);
        //    string angleString = odoArray[2].ToString().Substring(6, 11);
        //    float angleRad = float.Parse(angleString); // * 57.2958f;
        //    float xPreCalib = float.Parse(xString);
        //    float yPreCalib = float.Parse(yString);


        //    float angle = (angleRad * 57.2958f) + rotationCalib;

        //    //float x = (xPreCalib * (float)Math.Cos(angleRad)) + zCalib;
        //    //float y = (yPreCalib * (float)Math.Cos(angleRad)) + xCalib;

        //    float x = (xPreCalib) + zCalib;
        //    float y = (yPreCalib ) + xCalib;

        //    if (x != oldx || y != oldy || angle != oldAngle)
        //    {
        //        Debug.Log(yString + " " + xString + " " + angle);
        //        oldx = x;
        //        oldy = y;
        //        oldAngle = angle;
        //    }



        //    transform.position = new Vector3(y, -0.014f, x);
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}

        //if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == false)
        //{
        //    StartCoroutine(GetRequest("172.21.9.90/data/odometry"));
        //    //// [x,y,rot,vx,vy,omega,seq] distance in meters
        //    string[] odoArray = robotinoOdo.Split(',');
        //    string xString = odoArray[0].ToString().Substring(6, 11);
        //    string yString = odoArray[1].ToString().Substring(6, 11);
        //    string angleString = odoArray[2].ToString().Substring(6, 11);
        //    float angleRad = float.Parse(angleString); // * 57.2958f;
        //    float xPreCalib = float.Parse(xString);
        //    float yPreCalib = float.Parse(yString);


        //    float angle = (angleRad * 57.2958f) + rotationCalib;

        //    //float x = (xPreCalib * (float)Math.Cos(angleRad)) + zCalib;
        //    //float y = (yPreCalib * (float)Math.Cos(angleRad)) + xCalib;

        //    float x = (xPreCalib) + zCalib;
        //    float y = (yPreCalib ) + xCalib;

        //    if (x != oldx || y != oldy || angle != oldAngle)
        //    {
        //        Debug.Log(yString + " " + xString + " " + angle);
        //        oldx = x;
        //        oldy = y;
        //        oldAngle = angle;
        //    }



        //    transform.position = new Vector3(y, -0.014f, x);
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}


        //if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == true)
        //{

            if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true && rotating == false)
            {
                goStop = true;
            }

            if (goStop == true && percentLap < 100 && toIsland2 == true)
            {

                percentLap += 0.25f;
                percentLap += 0.15f;

                distanceTravelled = ((percentLap / 100) * 7.83683f);
                transform.position = robotinoPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                transform.rotation = robotinoPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + angle, 0f);
            }

            if (goStop == true && percentLap > 0 && toIsland1 == true)
            {

                percentLap -= 0.25f;
                percentLap -= 0.15f;
                distanceTravelled = ((percentLap / 100) * 7.83683f);
                transform.position = robotinoPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                transform.rotation = robotinoPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + angle, 0f);
            }



            if (turningPoint1 == true && rotated == false && toIsland2 == true)
            {
                rotating = true;
                goStop = false;
                angle += rotateSpeed;
                transform.eulerAngles = new Vector3(0, angle + 90, 0);

                if (angle == 360)
                {
                    goStop = true;
                    rotated = true;
                    rotating = false;
                }
            }

            if (turningPoint2 == true && rotated == false && toIsland1 == true)
            {
                rotating = true;
                goStop = false;
                angle -= rotateSpeed;
                transform.eulerAngles = new Vector3(0, angle, 0);

                if (angle == 180)
                {
                    goStop = true;
                    rotated = true;
                    rotating = false;
                }
            }

            if (percentLap > 50 && percentLap < 60)
            {
                rotated = false;
            }

        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "turningPointToIsland1":
                turningPoint2 = true;
                break;
            case "turningPointToIsland2":
                turningPoint1 = true;
                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "turningPointToIsland1":
                turningPoint2 = false;
                break;
            case "turningPointToIsland2":
                turningPoint1 = false;
                break;
        }

    }
}
