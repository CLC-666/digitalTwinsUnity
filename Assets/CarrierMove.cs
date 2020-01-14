using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = -13.8f;
    public float y = 49.5f;
    public float z = -1.82f;
    int caseSwitch = 2;
    float speed = 0.06f;

    void Start()
    {
        // Manual Station
        transform.position = new Vector3(x,y,z);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(x, y, z);



        switch (caseSwitch)
        {
            case 1: //Start turning from manual station to camera
                Debug.Log("Case 2");
                if (x < 21)
                {
                    x += 0.06f;
                }
                if (x > 21)
                {
                    transform.position = new Vector3(21f, y, z);
                }
                if (x >= 17) { 
                Quaternion target = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
                z += speed;
                }

                if (z >= 33) //start turning
                {
                    caseSwitch = 2;
                }

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
                break;

            case 2:
                Debug.Log("Case 3");
                if (z < 36)
                {
                    z += speed;
                }
                if (z > 36)
                {
                    transform.position = new Vector3(x, y, 36.21f);
                }
                Quaternion target2 = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime);
                x -= speed;
                
                if (x <= -13.5)
                {
                    caseSwitch = 3;
                }
                break;

            case 3:
                Debug.Log("Case 4");
                if (x > -17)
                {
                    x -= 0.06f;
                }
                if (x < -17)
                {
                    transform.position = new Vector3(-17.35f, y, z);
                }
                Quaternion target3 = Quaternion.Euler(0, 270, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target3, Time.deltaTime);
                z -= speed;

                if (z <= 2)
                {
                    caseSwitch = 4;
                }
                break;

            case 4:
                Debug.Log("Case 5");
                if (z > 0)
                {
                    z -= 0.06f;
                }
                if (z < 0)
                {
                    transform.position = new Vector3(x, y, -1.8f);
                }
                Quaternion target4 = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target4, Time.deltaTime);
                x += speed;


                if (x >= 0)
                {
                    caseSwitch = 1;
                }
                break;
        }




        

    }

}
