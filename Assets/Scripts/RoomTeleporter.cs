using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    // Generates a list, the list you can fill from unity.
    public List<Transform> rooms = new List<Transform>();
    private List<Transform> availableRooms = new List<Transform>();
    public bool isNearDoor = false;

    // Checks inputs the rooms that was added in unity in to the list.
    void Start()
    {
        availableRooms.AddRange(rooms);
    }

    // Allows the player to teleport to a room if there are rooms left, the player is near a door and then waits for player to press T.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && availableRooms.Count > 0 && isNearDoor)
        {
            TeleportPlayerToRandomRoom();
        }
    }

    // Randomizes what room you will get when you activate a door, transports the player to the selected room and then removes the room from selection.
    void TeleportPlayerToRandomRoom()
    {
        int randomIndex = Random.Range(0, availableRooms.Count);
        Transform roomToTeleport = availableRooms[randomIndex];

        Transform playerTransform = this.transform;
        playerTransform.position = roomToTeleport.position;

        availableRooms.RemoveAt(randomIndex);
    }

}