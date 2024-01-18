using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private SpawnManager spawnManager;
    private bool playerIsNear;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        playerIsNear = false;
    }

    private void Update()
    {
        // Check if the player is near, 'T' is pressed, and all enemies are defeated
        if (playerIsNear && Input.GetKeyDown(KeyCode.T) && spawnManager.AreAllEnemiesDefeated())
        {
            TriggerTeleportation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void TriggerTeleportation()
    {
        int nextRoomIndex = RoomManager.Instance.GetNextRoomIndex();
        RoomManager.Instance.TeleportPlayer(nextRoomIndex);
        spawnManager.DeactivateRoom();
    }
}
/*
{
    private SpawnManager spawnManager;
    private bool playerIsNear;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        playerIsNear = false;
    }

    private void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.T) && spawnManager.AreAllEnemiesDefeated())
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void TeleportPlayer()
    {
        int nextRoomIndex = RoomManager.Instance.GetNextRoomIndex();
        RoomManager.Instance.TeleportPlayer(nextRoomIndex);
        spawnManager.DeactivateRoom();
    }
}

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int nextRoomIndex = RoomManager.Instance.GetNextRoomIndex();
            RoomManager.Instance.TeleportPlayer(nextRoomIndex);
        }
    }
}
*/