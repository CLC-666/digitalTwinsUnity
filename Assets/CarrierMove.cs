using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = -13.8f;
    public float y = 49.5f;
    public float z = -5.25f;
    public bool aCatch = false;

    void Start()
    {
        // Manual Station
        transform.position = new Vector3(x,y,z);
    }

    // Update is called once per frame
    void Update()
    {
       posCatch();
       transform.position = new Vector3(x, y, z);
       Debug.Log(x);
    }

    void posCatch()
    {
        if (aCatch == false)
        {
            x += 0.06f;            
        }

        if (transform.position.x >= 3.7f && transform.position.x <= 4.3f)
        {
            aCatch = true;
            x = -13.8f;
            y = 49.5f;
            z = -5.25f;
        }
    }


}
