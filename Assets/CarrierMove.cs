using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = -13.8f;
    public float y = 49.5f;
    public float z = -1.82f;

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

        if (x >= 18.4 && x <= 21.3 && z <= 35)
        {
            Quaternion target = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
            z += 0.06f;
        }

        if (z >= 33.3 && z <= 36.5)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
            x -= 0.06f;
            z = 36.1f;
        }

        if (x >= 14 && x <= 14.4 && z <= 36.1)
        {
            Quaternion target = Quaternion.Euler(0, 270, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
            z -= 0.06f;
            x = -17.58f;
        }
    }

    void posCatch()
    {
        if (x <= 21.1 && z < 30)
        {
            x += 0.06f;
        }

        if (x > 21 && z < 30)
        {
            transform.position = new Vector3(20.2f, 49.5f, z);
        }



        //if (x <= 21 && z > 34)
        //{
        //    transform.position = new Vector3(x, 49.5f, 36.1f);
        //}
       

        if (transform.position.x >= -14.6f && transform.position.x <= -14f)
        {
            Debug.Log("First Induction Sensor");
        }

        if (transform.position.x >= 3.6f && transform.position.x <= 4.2f)
        {
            Debug.Log("Second Induction Sensor and Stop");
        }

        if (transform.position.x >= 15.9f && transform.position.x <= 16.5f)
        {
            Debug.Log("Third Induction Sensor");
        }
    }


}
