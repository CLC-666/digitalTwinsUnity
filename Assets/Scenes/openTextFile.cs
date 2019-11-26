using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class openTextFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string path = @"C:\Users\willi\Documents\Inventions\Unity\Augmentation\embeddedDigitalTwin\Assets\Scenes\test.txt";
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }
        }

        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            //while ((s = sr.ReadLine()) != null)
            //{
            //    Debug.Log(s);
            //}
        }
    }


}
