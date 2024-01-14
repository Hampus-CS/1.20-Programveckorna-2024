using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
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