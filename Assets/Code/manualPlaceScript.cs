using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manualPlaceScript : MonoBehaviour
{
    public GameObject workpiecePrefab;
    public GameObject[] workpieces;
    public bool run = false;
    public bool runPhysical = false;
    public bool generateWorkpiece = false;
    public int workpieceCounter = 0;
    public int carrierID = 0;
    public int switchCase = 0;
    int pauseTime = 50;
    int pauseCounter = 0;
    float speed = 0.0015f;
    float currentPos;
    float carrierPlate = 1.03f;
    public bool runPhysicalComplete = false;

    float workX = -6.6212f; //spawn position
    float workY = 1.0683f;
    float workZ = 0.0358f;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        workpieces = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(workpiecePrefab) as GameObject;
            clone.SetActive(false);
            clone.transform.position = new Vector3(workX, workY, workZ);
            clone.transform.rotation = Quaternion.Euler(0, 270f, 0);
            workpieces[i] = clone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (run == true)
        {
            if (spawned == false) { spawnNewWorkpiece(); spawned = true; }
            lowerWorkpiece();
            GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning = false;
        }

        if (run == false)
        {
            GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning = true;
        }

        if (runPhysical == true)
        {
            if (spawned == false) { spawnNewWorkpiece(); spawned = true; }
            lowerWorkpiecePhysical();
            GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning = false;
        }

        if (runPhysical == false)
        {
            GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualConvRunning = true;
            runPhysicalComplete = false;
        }
    }

    void lowerWorkpiecePhysical()
    {
        if (runPhysicalComplete == false)
            {
                workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, carrierPlate, workZ);
                Debug.Log("After manual conv running set to true");
                spawned = false;
                workpieceCounter++;
                runPhysicalComplete = true;
            }
        
        
    }

    void spawnNewWorkpiece()
    {
       
        workpieces[workpieceCounter].GetComponent<WorkpieceProperties>().carrierID = carrierID;
        workpieces[workpieceCounter].SetActive(true);
        generateWorkpiece = true;
        workpieceCounter++;
        
        if (workpieceCounter >= 10) { Debug.LogError("NO MORE WORKPIECES LEFT."); }
    }

    void lowerWorkpiece()
    {
        switch (switchCase)
        {
            case 0:
                currentPos = workY;
                if (pauseCounter < pauseTime) { pauseCounter++; }
                if (pauseCounter == pauseTime) { switchCase = 1; }
                break;

            case 1:
                if (currentPos > carrierPlate)
                {
                    currentPos -= speed;
                    workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, currentPos, workZ);
                }

                if (currentPos <= carrierPlate)
                {
                    workpieces[workpieceCounter - 1].transform.position = new Vector3(workX, carrierPlate, workZ);
                    run = false;
                    Debug.Log("After manual conv running set to true");
                    spawned = false;
                    workpieceCounter++;
                    switchCase = 0;
                }

                break;
        }
    }
}
