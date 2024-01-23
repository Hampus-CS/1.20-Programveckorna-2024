using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    PlayerCore thePlayerCore;
    Transform theTransform;
    Rigidbody2D theRigidbody;

    void Start()
    {
        thePlayerCore = gameObject.GetComponent<PlayerCore>();
        theTransform = gameObject.GetComponent<Transform>();
        theRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerCore.currentState == 0 || thePlayerCore.currentState == 2)
        {
            if (Input.GetMouseButton(1) && thePlayerCore.IsGrounded())
            {
                thePlayerCore.currentState = 2;

            }
            else
            {
                thePlayerCore.currentState = 0;
            }
        }
    }
}
