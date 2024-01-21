using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private bool isTargetAlive = true;
    private bool isCameraLocked = false;
    private Vector3 originalPosition;
    private Vector3 lockedPosition;

    public void ToggleCameraLock()
    {
        isCameraLocked = !isCameraLocked;

        if (!isCameraLocked)
        {
            lockedPosition = Vector3.zero;
        }
    }

    public void LockCamera(Vector3 position)
    {
        isCameraLocked = true;
        lockedPosition = position + new Vector3(0, 0, -10);
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    private void Start()
    {
        //shake = 5f;
    }

    private void LateUpdate()
    {
        if (!isTargetAlive || isCameraLocked || target == null)
        {
            return;
        }

        Vector3 newPosition = target.position;
        newPosition.z = target.position.z - 10;

        if (!isCameraLocked)
        {
            transform.position = newPosition;
        }
        else
        {
            transform.position = lockedPosition;
        }
    }
}


/*

{
    private Transform target;
    private bool isTargetAlive = true;
    private bool isCameraLocked = false;
    private Vector3 originalPosition;
    private Vector3 lockedPosition;

    public float shake = 0f;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    private void Start()
    {
        //shake = 5f;
    }

    public void ToggleCameraLock()
    {
        isCameraLocked = !isCameraLocked;
    }

    public void SetCameraPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void LateUpdate()
    {
        if (!isTargetAlive || isCameraLocked || target == null)
        {
            return;
        }

        if (isCameraLocked)
        {
            transform.position = lockedPosition; // Lock the camera to the locked position
        }
        
        Vector3 newPosition = target.position;
        newPosition.z = target.position.z - 10;

        transform.position = newPosition;
    }

}

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