using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class magFrontLiftScript : MonoBehaviour
{
    public bool run = false;
    public bool generateWorkpiece = false;
    public GameObject workpiecePrefab;

    private void Update()
    {
        if (run == true && generateWorkpiece == false)
        {
            GameObject workpiece = Instantiate(workpiecePrefab) as GameObject;
            generateWorkpiece = true;
        }

        if (run == false)
        {
            generateWorkpiece = false;
        }
    }

}