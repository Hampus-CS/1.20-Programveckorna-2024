using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpawnPoint : MonoBehaviour
{
    public GameObject room;
    bool done = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && done == false)
        {
            
            room.GetComponent<SpawnManager>().ActivateRoom();
            done = true;

        }
    }

    public void ResetSpawnPoint()
    {
        done = false;
        Debug.Log("SpawnPoint reset");
    }

}
