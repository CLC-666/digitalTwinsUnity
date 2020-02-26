using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarrierMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = -6.6214f;
    public float y = 0.979f;
    public float z = 0.264f;
    public string position = "";
    int mainSwitch = 1;
    int magFrontSwitch = 0;
    int manualSwitch = 1;
    int c1 = 0;
    int c2 = 0;
    int c3 = 0;
    int c4 = 0;
    public int station;
    public int fInduc;
    public int mInduc;
    public int eInduc;
    public int carriageReleased;
    public float speed = 0.09f;

    void Start()
    {
        // Manual Station
        transform.position = new Vector3(x,y,z);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles = new Vector3(0, 0, 0);
        first();
    }

    void first() { 

        transform.position = new Vector3(x, y, z);

        //GameObject locData = GameObject.Find("firstIsland");
        //GameObject locationData = locData.GetComponent<ServerClient>();
        string locationData = GameObject.Find("CornerWithScript").GetComponent<ServerClient>().locationData;
        Debug.Log("Location Data" + locationData);
        //char[] chars = locationData.ToCharArray();
        if (locationData.Length > 1)
        {
            string s = locationData[0].ToString().Trim();
            string f = locationData[2].ToString().Trim();
            string m = locationData[4].ToString().Trim();
            string t = locationData[8].ToString().Trim();

            station = Convert.ToInt32(s);
            fInduc = Convert.ToInt32(f);
            mInduc = Convert.ToInt32(m);
            eInduc = Convert.ToInt32(t);
            carriageReleased = Convert.ToInt32(locationData[6].ToString().Trim());
        }



        //switch (magFrontSwitch)
        //{
        //    case 1:
        //        if (fInduc == 1)
        //        {
        //            x = -17.1f;
        //            magfrontSwitch = 2;
        //        }
        //        break;

        //    case 2:
        //        if (mInduc == 1)
        //        {
        //            x = 2.34f;
        //            magFrontSwitch = 3;
        //        }
        //        break;
        //    case 3:
        //        speed = 0;
        //        if (carriageReleased == 1)
        //        {
        //            magFrontSwitch = 4;

        //        }
        //        break;
        //    case 4:
        //        speed = 0.09f;

        //        if (eInduc == 1)
        //        {
        //            x = 14.52f;
        //            magFrontSwitch = 1;
        //        }
        //        break;
        //}

        Debug.Log("speed" + speed);

        switch (manualSwitch)
        {
            case 1:
                if (fInduc == 1)
                {
                    z = 0.4314f;
                    manualSwitch = 2;
                }
                break;

            case 2:
                if (mInduc == 1)
                {
                    x = 2.34f;
                    manualSwitch = 3;
                }
                break;
            case 3:
                speed = 0;
                if (carriageReleased == 1)
                {
                    manualSwitch = 4;

                }
                break;
            case 4:
                speed = 0.09f;

                if (eInduc == 1)
                {
                    x = 14.52f;
                    manualSwitch = 1;
                }
                break;
        }

        switch (mainSwitch)
        {
            case 1: //Start turning from manual station to camera
                Debug.Log("Case 1 " + c1);
                c1 += 1;
                if (x < 21)
                {
                    z = -1.8f;
                    x += speed;
                }
                if (x > 21)
                {
                    transform.position = new Vector3(21f, y, z);
                }
                if (x >= 17)
                {
                    Quaternion target = Quaternion.Euler(0, 90, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
                    z += speed;
                }

                if (z >= 33) //start turning
                {
                    mainSwitch = 2;
                }
                break;

            case 2:
                Debug.Log("Case 2 " + c2);
                c2 += 1;
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
                    mainSwitch = 3;
                }
                break;

            case 3:
                Debug.Log("Case 3 " + c3);
                c3 += 1;
                if (x > -17)
                {
                    x -= speed;
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
                    mainSwitch = 4;
                }
                break;

            case 4:
                Debug.Log("Case 4 " + c4);
                c4 += 1;
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
                    mainSwitch = 1;
                }
                break;


        }






    }

}
