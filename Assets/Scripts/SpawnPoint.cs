using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpawnPoint : MonoBehaviour
{
    public GameObject room;
    public GameObject main_camera;
    public Transform current_camera;

    bool done = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && done == false)
        {
            
            room.GetComponent<SpawnManager>().ActivateRoom();
            done = true;

        }

        main_camera.GetComponent<CameraController>().current_camera = current_camera;
    }

    public void ResetSpawnPoint()
    {
        done = false;
        Debug.Log("SpawnPoint reset");
    }

}
