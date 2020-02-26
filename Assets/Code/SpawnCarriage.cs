using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnCarriage : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] gos;

    public int station;
    public int fInduc;
    public int mInduc;
    public int eInduc;
    public int carriageReleased;
    public int carrierID;
    public float speed = 0.09f;
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

    //void Awake()
    //{
    //    gos = new GameObject[1];
    //    for (int i = 0; i < gos.Length; i++)
    //    {
    //        GameObject clone = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
    //        gos[i] = clone;
    //    }
    //}
    void Start()
    {
        gos = new GameObject[1];
        for (int i = 0; i < gos.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            gos[i] = clone;
        }
        gos[0].SetActive(true);
        gos[0].transform.eulerAngles = new Vector3(0f, 270f, 0f);
        //gos[0].transform.eulerAngles = new Vector3(0f, 270f, 0f);
        gos[0].transform.position = new Vector3(x, y, z);
    }

    void Update()
    {
        gos[0].transform.position = new Vector3(x, y, z);

        //GameObject locData = GameObject.Find("firstIsland");
        //GameObject locationData = locData.GetComponent<ServerClient>();
        string locationDataManual = GameObject.Find("firstIsland").GetComponent<manualClient>().locationData;
        string locationDataMagFront = GameObject.Find("firstIsland").GetComponent<magFrontClient>().locationData;
        Debug.Log("Location Data" + locationDataManual + locationDataMagFront);
        //char[] chars = locationData.ToCharArray();
        if (locationDataManual.Length > 1) //2,0,0,0,0,0
        {
            string s = locationDataManual[0].ToString().Trim();
            string f = locationDataManual[2].ToString().Trim();
            string m = locationDataManual[4].ToString().Trim();
            string t = locationDataManual[8].ToString().Trim();

            station = Convert.ToInt32(s);
            fInduc = Convert.ToInt32(f);
            mInduc = Convert.ToInt32(m);
            eInduc = Convert.ToInt32(t);
            carriageReleased = Convert.ToInt32(locationDataManual[6].ToString().Trim());
            carrierID = Convert.ToInt32(locationDataManual[10].ToString().Trim());
        }

    }
}
