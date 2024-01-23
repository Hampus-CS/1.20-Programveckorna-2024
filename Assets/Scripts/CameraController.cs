using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using System.Collections.Specialized;
using UnityEditorInternal;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private bool isTargetAlive = true;
    private RoomManager roomManager;
    public float shake = 0f;
    public Transform current_camera;
    public Transform transition;
    public int side = -1;
    public int timer = 0;
    public SpawnManager spawnManager;
    private void Start()
    {
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
        if (!isTargetAlive)
        {
            return;
        }
        */
        transform.position += (new Vector3(current_camera.position.x + Random.Range(-shake, shake), current_camera.position.y + Random.Range(-shake, shake), -10) - transform.position)*0.1f;
    }
}

/*
Old camera code, made it so the camera follows the player.
{

    Transform target;
    Vector3 velocity=Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime;

    public Vector3 positionOffset;

    public float shake = 0f;

    private bool isTargetAlive = true;

    private void Awake()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    
    }

    private void Start()
    {
        shake = 5f;
    }

    private void LateUpdate()
    {
        
        // Check if the target is destroyed
        if (target == null)
        {
            isTargetAlive = false;
        }

        // If the target is destroyed, stop updating the camera's position
        if (!isTargetAlive)
        {
            return;
        }

        // Screen Shake
        shake += (0f - shake) * 0.1f;

        // Camera following the player with SmoothDamp
        Vector3 targetPosition = target.position+ positionOffset + new Vector3(0f + Random.Range(-shake, shake), 0f + Random.Range(-shake, shake), 0f); ;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    
    }

}
*/