using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotinoScript : MonoBehaviour
{
    float startX = -4.964f;
    float startY = -0.014f;
    float startZ = 0.072f;
    public bool robotinoFirstIsland = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().robotinoCarrierStop == true) {
            //move
        }
      
    }

    
}
