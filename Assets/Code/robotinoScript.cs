using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

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

    // Start is called before the first frame update
    void Start()
    {
        goStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true && rotating == false)
        {
            goStop = true;
        }

        if (goStop == true && percentLap < 100 && toIsland2 == true)
        {
            percentLap += 0.25f;
            distanceTravelled = ((percentLap / 100) * 7.83683f);
            transform.position = robotinoPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = robotinoPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + angle, 0f);
        }

        if (goStop == true && percentLap > 0 && toIsland1 == true)
        {
            percentLap -= 0.25f;
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
            transform.eulerAngles = new Vector3(0, angle + 90, 0);

            if (angle == 0)
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
