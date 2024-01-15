using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Collider2D platformCollider;
    public Transform player;
    /*
    private void Start()
    {
        platformCollider1 = GetComponent<Collider2D>();
        platformCollider2 = GetComponent<Collider2D>();
        DisablePlatformCollider();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the platform trigger zone.");
            EnablePlatformCollider();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the platform trigger zone.");
            DisablePlatformCollider();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && (platformCollider2.enabled == true))
        {
            FallThroughPlatform();
        }
    }

    private void FallThroughPlatform()
    {
        // Check if the player is currently on the platform
        if (platformCollider2.enabled)
        {
            DisablePlatformCollider();
            //Invoke("EnablePlatformCollider", 0.1f); // Delayed re-enable to prevent immediate re-entry
        }
    }
    */
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
        Debug.Log("True");
        platformCollider.enabled = true;
    }

    private void DisablePlatformCollider()
    {
        Debug.Log("False");
        platformCollider.enabled = false;

    }
}