using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private SpawnManager spawnManager;
    public bool playerIsNear;
    public bool playerTeleport;
    public GameObject camera;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        playerIsNear = false;
    }

    private void Update()
    {
            DoorInteraction();
    }

    private void DoorInteraction()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.T) && spawnManager.AreAllEnemiesDefeated())
        {
            camera.SetActive(false);
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
            PlayerMovement.playerHealth++;
            ScoreTracker.Score++;
    }

}