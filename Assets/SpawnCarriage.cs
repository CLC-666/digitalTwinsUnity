using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarriage : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] gos;

    void Awake()
    {
        gos = new GameObject[10];
        for (int i = 0; i < gos.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            gos[i] = clone;
        }
    }
}
