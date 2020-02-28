using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarrierMove : MonoBehaviour
{
    public float x = -6.6214f;
    public float y = 0.979f;
    public float z = 0.4314f;
    public int carrierID = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(x, y, z);
        y = y + 0.01f;
    }

   

}
