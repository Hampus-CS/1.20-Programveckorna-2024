using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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