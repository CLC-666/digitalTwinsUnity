using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class follower : MonoBehaviour
{
    public PathCreator firstIsland;
    public float speed = 0;
    public float distanceTravelled;
    public float percentLap;

    public int carrierID;
    public int spawnLocation;

    private void Start()
    {
        if (spawnLocation == 1) { percentLap = 67; }
        if (spawnLocation == 2) { percentLap = 92; }
        if (spawnLocation == 3) { percentLap = 17; }
        if (spawnLocation == 4) { percentLap = 36.5f; }
    }

    private void Update()
    {

        //3.753859
        distanceTravelled = (percentLap / 100) * -3.753859f;

        //distanceTravelled -= speed * Time.deltaTime;

        transform.position = firstIsland.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = firstIsland.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 270, 0f);

        //Debug.Log(firstIsland.path.GetPointAtDistance(distanceTravelled));


    }
}
