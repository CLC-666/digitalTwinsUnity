using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float horizontalInput = 0f;
    private float movementSpeed = 0.1f;
    float rotation = 0;
    // Update is called once per frame
    void Update()
    {
        string path = @"C:\Users\willi\Documents\Inventions\Unity\Augmentation\embeddedDigitalTwin\Assets\Scenes\test.txt";
        
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                //get the Input from Horizontal axis
                horizontalInput = float.Parse(s);
                

            }
        }
        

     
        //if (transform.localPosition.y < 100)
        //{
            // update the position
            Debug.Log(horizontalInput);
            transform.position = transform.position + new Vector3(horizontalInput * movementSpeed, 0, 0);
            //Debug.Log(transform.position);
            Debug.Log(transform.localPosition);
            //transform.position = new Vector3(1, 1, 1);
            transform.Rotate(0, rotation, 0);
        //}

       

        
    }
}
