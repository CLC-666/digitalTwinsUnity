using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class robotinoScript : MonoBehaviour
{
    float startX = -4.964f;
    float startY = -0.014f;
    float startZ = 0.072f;
    public bool robotinoFirstIsland = false;
    public PathCreator robotinoPath;
    public float distanceTravelled;
    public float percentLap = 0;
    public bool goStop = false;
    public string caseSwitch;
    public bool turningPoint = false;
    public float angle = 180;
    public float rotateSpeed = 1;
    public bool rotated = false;
    bool rotating = false;

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

        if (goStop == true && percentLap < 100)
        {
            percentLap += 0.25f;
            distanceTravelled = ((percentLap / 100) * 7.83683f);
            transform.position = robotinoPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = robotinoPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + angle, 0f);
        }



        if (turningPoint == true && rotated == false)
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

        if (percentLap > 50)
        {
            rotated = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "turningPoint":
                turningPoint = true;
                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        caseSwitch = other.gameObject.name;

        switch (caseSwitch)
        {
            case "turningPoint":
                turningPoint = false;
                break;
        }

    }
}
