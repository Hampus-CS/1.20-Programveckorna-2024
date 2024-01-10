using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    // List of room positions
    public List<Transform> rooms = new List<Transform>();

    // List to keep track of available rooms
    private List<Transform> availableRooms = new List<Transform>();

    void Start()
    {
        // Initialize available rooms
        availableRooms.AddRange(rooms);
    }

    void Update()
    {
        // Check for a key press and if there are still available rooms
        if (Input.GetKeyDown(KeyCode.T) && availableRooms.Count > 0)
        {
            TeleportPlayerToRandomRoom();
        }
    }

    void TeleportPlayerToRandomRoom()
    {
        // Select a random room
        int randomIndex = Random.Range(0, availableRooms.Count);
        Transform roomToTeleport = availableRooms[randomIndex];

        // Teleport the player to the room
        Transform playerTransform = this.transform; // Assuming this script is attached to the player
        playerTransform.position = roomToTeleport.position;

        // Remove the selected room from the list of available rooms
        availableRooms.RemoveAt(randomIndex);
    }
}