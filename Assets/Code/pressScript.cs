using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressScript : MonoBehaviour
{
    float initPosX = -8.2721f;
    float initPosY = 0.9480001f;
    float initPosZ = 3.5912f;
    float y;
    float bottomPos = 0.9371f;
    public float speed = 0.0015f;
    public bool run = false;
    public float caseSwitch = 0;
    public float pressSpeed = 1;
    float startTime;

    void Start()
    {
        transform.position = new Vector3(initPosX, initPosY, initPosZ);
        y = initPosY;
    }

    void Update()
    {
        switch (caseSwitch)
        {
            case 0:
                if (run == true)
                {
                    caseSwitch = 5;
                }
                break;

            case 5:
                if (transform.position.y == initPosY && GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressWorkpieceCheck == true)
                {
                    caseSwitch = 10;
                }

                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressWorkpieceCheck == false)
                {
                    Debug.Log("No workpiece available.");
                    run = false;
                }

                break;


            case 10:
                y -= speed;
                transform.position = new Vector3(initPosX, y, initPosZ);

                if (transform.position.y <= bottomPos)
                {
                    caseSwitch = 20;
                }
                break;

            case 20:
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentPressureN += pressSpeed;
                Debug.Log(GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentPressureN);

                if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentPressureN == GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressTargetPressureN)
                {
                    caseSwitch = 30;
                }
                break;

            case 30:
                StartCoroutine(waitFunction());
                startTime = Time.time;
                caseSwitch = 31; //timer will increase this to 32.
                break;

            case 31:
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentTime = Time.time - startTime;
                Debug.Log("Pressing at required pressure for: " + GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentTime + " seconds");
                break;
            case 32:
                y += speed;
                transform.position = new Vector3(initPosX, y, initPosZ);

                if (transform.position.y >= initPosY)
                {
                    caseSwitch = 40;
                }
                break;
            case 40:
                run = false;
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentPressureN = 0;
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressCurrentTime = 0;
                startTime = 0;
                caseSwitch = 0;
                break;

        }



    }

    IEnumerator waitFunction()
    {
        yield return new WaitForSeconds(GameObject.Find("Main Camera").GetComponent<runInSimMode>().pressTargetTime);
        caseSwitch += 1;
    }
}
