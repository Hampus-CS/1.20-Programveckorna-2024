using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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