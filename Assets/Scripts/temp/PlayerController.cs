using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * OLD
public class PlayerController : MonoBehaviour
{
    private RoomManager roomManager;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        // Ensure that roomManager is not null before accessing currentRoomIndex.
        if (roomManager != null)
        {
            transform.position = roomManager.playerSpawnPoint.position;
        }
    }

    private void Update()
    {
        if (roomManager != null && roomManager.CurrentRoomIndex >= 0) // Access it using the property or field name.
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Room currentRoom = roomManager.rooms[roomManager.CurrentRoomIndex]; // Access it using the property or field name.
                if (currentRoom.AllEnemiesDefeated())
                {
                    currentRoom.timesVisited++;
                    roomManager.SwitchRoom();
                }
            }
        }
    }

}
*/