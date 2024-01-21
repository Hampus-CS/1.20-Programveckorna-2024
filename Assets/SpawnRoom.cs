using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpawnRoom : MonoBehaviour
{

    public GameObject spawncamera;


    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (spawncamera != null)
            {
                spawncamera.SetActive(false);
            }
        }
    }

}