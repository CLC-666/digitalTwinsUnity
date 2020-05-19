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
    float topPosWorkpiece = 1.082f;
    float bottomPosWorkpiece = 1.0505f;
    float carrierPlateWorkpiece = 1.0362f; //1.0372f;
    float currentPosWorkpiece;

    float workX = -6.1316f;
    float workY = 1.082f;
    float workZ = 0.5605f;

    float liftX = -6.0778f;
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
            clone.transform.rotation = Quaternion.Euler(90f, 0, 0);
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
            if (Top == false && Bottom == true) { transform.position = new Vector3(liftX, bottomPos,liftZ); currentPos = bottomPos; currentPosWorkpiece = bottomPosWorkpiece; }
            if (Top == true && Bottom == true) { Debug.LogError("MagFrontLift Sensor data is incorrect!"); }
            secondStart = true;
        }
        
        if (run == true)
        {
            spawnNewWorkpiece();
            lowerWorkpiece();
        }

        if (run == false){generateWorkpiece = false; }
    }

    void lowerWorkpiece()
    {
        switch (switchCase) {

            case 0:
                if (pauseCounter < pauseTime){ pauseCounter++;}
                if (pauseCounter == pauseTime){ switchCase = 1;}
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
                    switchCase = 0;
                }
                break;

        }   
    }


    void spawnNewWorkpiece()
    {
        if (workpieceCounter < 10 && generateWorkpiece == false)
        {
            workpieces[workpieceCounter].GetComponent<WorkpieceProperties>().carrierID = carrierID;
            workpieces[workpieceCounter].SetActive(true);
            generateWorkpiece = true;
            workpieceCounter++;
        }

        if (workpieceCounter >= 10) { Debug.LogError("NO MORE WORKPIECES LEFT."); }
    }

}