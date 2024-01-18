using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class StartTeleporter : MonoBehaviour
{
    public Transform spawnPoint; // Assign the player's spawn point in the Inspector.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.T))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        if (spawnPoint != null)
        {
            playerTransform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogError("Spawn point not assigned to the door.");
        }
    }
}