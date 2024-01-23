using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerCore playerCore;
    float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCore = gameObject.GetComponent<PlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCore.attackTimer <= 0f && playerCore.currentState != 2)
        {
            speed += ((Input.GetAxisRaw("Horizontal") * 1f) - speed) * 0.035f;
        }
        else
        {
            speed += (0 - speed) * 0.1f;
        }
    }
}
