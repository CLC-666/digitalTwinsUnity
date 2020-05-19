using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkpieceProperties : MonoBehaviour
{

    public int carrierID;
    Vector3 carrierPosition;
    GameObject carrier;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("carrier"))
        {
            Debug.Log("collision " + other.gameObject.name);
            carrier = other.gameObject;
            gameObject.transform.parent = carrier.transform;
        }
    }


}
