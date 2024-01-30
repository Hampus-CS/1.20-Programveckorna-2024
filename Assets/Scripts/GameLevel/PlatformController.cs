using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Collider2D platformCollider;
    public Transform player;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Vector2 player_position = player.transform.position;
            Vector2 offset_position = new Vector2(0f, 0.1f);
            Vector2 position = transform.position;
            position += offset_position;
            if (player_position.y > position.y)
            {
                if (Input.GetKey("s"))
                {
                    DisablePlatformCollider();
                }
                else
                {
                    EnablePlatformCollider();
                }
            }
            else
            {
                DisablePlatformCollider();
            }
        }
    }

    private void EnablePlatformCollider()
    {
        //Debug.Log("True");
        platformCollider.enabled = true;
    }

    private void DisablePlatformCollider()
    {
        //Debug.Log("False");
        platformCollider.enabled = false;

    }
}