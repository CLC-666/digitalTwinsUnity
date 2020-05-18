using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class runInSimMode : MonoBehaviour
{
    public int caseSwitch = 0;

    public bool manualStartInduction = false;
    public bool manualStopInduction = false;
    public bool manualEndInduction = false;
    public bool camInspectStartInduction = false;
    public bool camInspectStopInduction = false;
    public bool camInspectEndInduction = false;
    public bool codesys1StopInduction = false;
    public bool magFrontStartInduction = false;
    public bool magFrontStopInduction = false;
    public bool magFrontEndInduction = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Prog();
    }

    void Prog()
    {
        switch (caseSwitch)
        {
            case 0:

                

                break;
        }
    }
}
