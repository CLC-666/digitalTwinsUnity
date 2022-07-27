using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class magFrontLiftScript : MonoBehaviour
{
    public bool run = false;
    public bool generateWorkpiece = false;
    public GameObject workpiecePrefab;
    public GameObject[] workpieces;
    bool secondStart = false;
    int counter = 0;
    public int carrierID = 0;
    float topPos = 0.9509f;
    float bottomPos = 0.9121f;
    float currentPos = 0;
    public int switchCase = 0;
    float speed = 0.0015f;
    int workpieceCounter = 0;
    int pauseTime = 50;
    int pauseCounter = 0;
    float topPosWorkpiece = 1.0761f;
    float bottomPosWorkpiece = 1.0372f;
    float carrierPlateWorkpiece = 1.0262f; //1.0372f;
    float currentPosWorkpiece;

    float workX = -6.1287684179f; //spawn position
    float workY = 1.0761f;
    float workZ = 0.5605f;

    float liftX = -6.0778f; //spawn position
    float liftY = 0.959f;
    float liftZ = 0.5593f;

    private void Start()
    {
        transform.position = new Vector3(liftX, liftY, liftZ);
        workpieces = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(workpiecePrefab) as GameObject;
            clone.SetActive(false);
            clone.transform.position = new Vector3(workX, workY, workZ);
            clone.transform.rotation = Quaternion.Euler(270f, 0, 0);
            workpieces[i] = clone;
        }
    }

    private void Update()
    {
        if (secondStart == false)
        {
            bool Top = GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop;
            bool Bottom = GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontBottom;
            if (Top == true && Bottom == false) { transform.position = new Vector3(liftX, topPos, liftZ); currentPos = topPos; currentPosWorkpiece = topPosWorkpiece; }
            if (Top == false && Bottom == true) { transform.position = new Vector3(liftX, bottomPos, liftZ); currentPos = bottomPos; currentPosWorkpiece = bottomPosWorkpiece; }
            if (Top == true && Bottom == true) { Debug.LogError("MagFrontLift Sensor data is incorrect!"); }
            secondStart = true;
        }

        if (run == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == true)
        {
            GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning = false;
            spawnNewWorkpiece();
            lowerWorkpiece();
        }

        if (run == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == false)
        {
            spawnNewWorkpiece();
            lowerWorkpiece();
        }

        if (run == false) { generateWorkpiece = false; }
    }

    void lowerWorkpiece()
    {
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == true)
        {
            switch (switchCase)
            {

                case 0:
                    if (pauseCounter < pauseTime) { pauseCounter++; }
                    if (pauseCounter == pauseTime) { switchCase = 1; }
                    break;

                case 1:
                    //Debug.Log("case 1");
                    pauseCounter = 0;
                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop == true)
                    {
                        switchCase = 2;
                    }
                    break;

                case 2:
                    //Debug.Log("case 2");
                    currentPos -= speed; //going down
                    currentPosWorkpiece -= speed;
                    transform.position = new Vector3(liftX, currentPos, liftZ);
                    workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, currentPosWorkpiece, workZ);

                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontBottom == true)
                    {
                        switchCase = 3;
                    }
                    break;

                case 3:
                    if (pauseCounter < pauseTime - 30) { pauseCounter++; }
                    if (pauseCounter >= pauseTime - 30) { switchCase = 4; }
                    break;

                case 4:
                    pauseCounter = 0;
                    currentPos += speed; //going up
                    transform.position = new Vector3(liftX, currentPos, liftZ);

                    if (currentPosWorkpiece > carrierPlateWorkpiece)
                    {
                        currentPosWorkpiece -= speed;
                        workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, currentPosWorkpiece, workZ);
                    }

                    if (currentPosWorkpiece <= carrierPlateWorkpiece)
                    {
                        workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, carrierPlateWorkpiece, workZ);
                    }


                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop == true)
                    {
                        run = false;
                        GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning = true;
                        switchCase = 0;
                    }
                    break;
            }
        }

        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == false)
        {
            switch (switchCase)
            {

                case 0:
                    GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning = false;
                    switchCase = 1; 
                    break;

                case 1:
                    //Debug.Log("case 1");
                    pauseCounter = 0;
                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop == true && GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontLiftDownQ == true)
                    {
                        switchCase = 2;
                    }

                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontCarrierRelease == true)
                    {
                        switchCase = 0;
                        run = false;
                    }
                    break;

                case 2:
                    //Debug.Log("case 2");
                    currentPos -= speed; //going down
                    currentPosWorkpiece -= speed;
                    transform.position = new Vector3(liftX, currentPos, liftZ);
                    workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, currentPosWorkpiece, workZ);

                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontBottom == true)
                    {
                        switchCase = 3;
                    }
                    break;

                case 3:
                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontLiftUpQ == true)
                    {
                        switchCase = 4;
                    }
                    break;

                case 4:
                    pauseCounter = 0;
                    currentPos += speed; //going up
                    transform.position = new Vector3(liftX, currentPos, liftZ);

                    if (currentPosWorkpiece > carrierPlateWorkpiece)
                    {
                        currentPosWorkpiece -= speed;
                        workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, currentPosWorkpiece, workZ);
                    }

                    if (currentPosWorkpiece <= carrierPlateWorkpiece)
                    {
                        workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, carrierPlateWorkpiece, workZ);
                    }


                    if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontTop == true)
                    {
                        run = false;
                        GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontConvRunning = true;
                        switchCase = 0;
                    }
                    break;
            }
        }
    }


    void spawnNewWorkpiece()
    {
        if (workpieceCounter < 10 && generateWorkpiece == false)
        {
            //workpieces[workpieceCounter].GetComponent<WorkpieceProperties>().carrierID = carrierID;
            workpieces[workpieceCounter].GetComponent<WorkpieceProperties>().carrierID = 1; //THE ABOVE LINE ONLY WORKS IN SIM, CARRIER ID IS BROADCAST AT THE POINT CARRIER RELEASED IS SENT ON THE PHYSICAL SYSTEM.
            workpieces[workpieceCounter].SetActive(true);
            generateWorkpiece = true;
            workpieceCounter++;
        }

        if (workpieceCounter >= 10) { Debug.LogError("NO MORE WORKPIECES LEFT."); }
    }

}