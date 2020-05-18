using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class follower : MonoBehaviour
{
    public PathCreator firstIsland;
    public float speed = 5;
    public float distanceTravelled;
    public float percentLap;

    private void Update()
    {
        //3.753859
        //distanceTravelled = (percentLap / 100) * 3.753859f;

        distanceTravelled -= speed * Time.deltaTime;

        transform.position = firstIsland.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);

        //Debug.Log(firstIsland.path.GetPointAtDistance(distanceTravelled));


    }
}
