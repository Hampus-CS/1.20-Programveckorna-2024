using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


/*
 * OLD
public class DoorCollider : MonoBehaviour
{
    private RoomManager roomManager;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        // Ensure that roomManager is not null before accessing currentRoomIndex.
        if (roomManager != null)
        {
            // You can access currentRoomIndex using the property or field name.
            int currentRoomIndex = roomManager.CurrentRoomIndex;
            Debug.Log("Current Room Index: " + currentRoomIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You can access currentRoomIndex using the property or field name.
            int currentRoomIndex = roomManager.CurrentRoomIndex;
            Debug.Log("isNearDoor=true, Current Room Index: " + currentRoomIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You can access currentRoomIndex using the property or field name.
            int currentRoomIndex = roomManager.CurrentRoomIndex;
            Debug.Log("isNearDoor=false, Current Room Index: " + currentRoomIndex);
        }
    }
}
/*
public class DoorCollider : MonoBehaviour
{
    private RoomManager roomManager;
    private bool canSwitchRoom = false;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    private void Update()
    {
        if (canSwitchRoom && Input.GetKeyDown(KeyCode.E))
        {
            Room currentRoom = roomManager.rooms[roomManager.currentRoomIndex];
            if (currentRoom.AllEnemiesDefeated())
            {
                currentRoom.timesVisited++;
                roomManager.SwitchRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canSwitchRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canSwitchRoom = false;
        }
    }
}


public class DoorCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                other.GetComponent<RoomTeleporter>().isNearDoor = true;
                Debug.Log("isNearDoor=true");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RoomTeleporter>().isNearDoor = false;
            Debug.Log("isNearDoor=false");
        }
    }
}
/*{
    // Checks if player is near door and if so then the varible isNearDoor is activated for the RoomTeleporter.cs script.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RoomTeleporter>().isNearDoor = true;
            Debug.Log("isNearDoor=true");
        }
    }

    // Checks if the player leaves the door and if so then the varible isNearDoor is deactivated for the RoomTeleporter.cs script.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RoomTeleporter>().isNearDoor = false;
            Debug.Log("isNearDoor=false");
        }
    }
}
*/