using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camInspectScript : MonoBehaviour
{
    public bool run = false;
    public int carrierID = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (run == true)
        {
            Debug.Log("Part is correct.");
            run = false;
        }
    }
}
