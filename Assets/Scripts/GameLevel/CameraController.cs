using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class CameraController : MonoBehaviour
{
    private Transform target;
    //If errors of the Player being dead appear then enable this:
    //private bool isTargetAlive = true;
    private RoomManager roomManager;
    public float shake = 0f;
    public Transform current_camera;
    public Transform transition;
    public int side = -1;
    public int timer = 0;
    public SpawnManager spawnManager;
    public float shake_amount = 1f;
    private void Start()
    {
        shake_amount = PlayerPrefs.GetFloat("ScreenShake");

        roomManager = RoomManager.Instance;
        shake = 5f;
        transition.position = new Vector3(transform.position.x + (45 * side), transform.position.y, 10f);
    }

    private void LateUpdate()
    {
        transition.position += (new Vector3(transform.position.x + (45 * side), transform.position.y, 10f) - transition.position) * 0.035f;

        if(timer == 60)
        {
            int nextRoomIndex = RoomManager.Instance.GetNextRoomIndex();
            RoomManager.Instance.TeleportPlayer(nextRoomIndex);
            spawnManager.DeactivateRoom();
        }

        if (timer == 1)
        {
            side = 1;
        }

        if (timer > 0) timer--;

        shake += (0f - shake) * 0.1f;
        /*
        //If errors of the Player being dead appear then enable this:
        if (!isTargetAlive)
        {
            return;
        }
        */
        transform.position += (new Vector3(current_camera.position.x + Random.Range(-shake*shake_amount, shake * shake_amount), current_camera.position.y + Random.Range(-shake * shake_amount, shake * shake_amount), -10) - transform.position)*0.1f;
    }
}