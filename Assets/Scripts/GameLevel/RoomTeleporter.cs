using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomTeleporter : MonoBehaviour
{
    
    public List<Transform> rooms = new List<Transform>();
    private Dictionary<Transform, float> roomWeights = new Dictionary<Transform, float>();
    public bool isNearDoor = false;
    public static event Action<Transform> OnRoomTeleport;

    // Initialises a weight system and the rooms
    void Start()
    {
        foreach (var room in rooms)
        {
            roomWeights[room] = 1.0f; // All rooms start with the same weight
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && rooms.Count > 0 && isNearDoor)
        {
            TeleportPlayerToRandomRoom();
        }
    }

    void TeleportPlayerToRandomRoom()
    {
        Transform roomToTeleport = GetWeightedRandomRoom();
        Transform playerTransform = this.transform;
        playerTransform.position = roomToTeleport.position;

        UpdateRoomWeights(roomToTeleport);
        OnRoomTeleport?.Invoke(roomToTeleport);
    }

    // Choosing a room based on the weight system
    Transform GetWeightedRandomRoom()
    {
        float totalWeight = 0f;
        foreach (var weight in roomWeights.Values)
        {
            totalWeight += weight;
        }

        float randomPoint = Random.value * totalWeight;

        foreach (var room in rooms)
        {
            if (randomPoint < roomWeights[room])
                return room;
            randomPoint -= roomWeights[room];
        }

        return null; // Should not be needed but is there to prevent possible bugs.
    }

    // Updates the weights after selecting a room
    void UpdateRoomWeights(Transform visitedRoom)
    {
        foreach (var room in rooms)
        {
            if (room == visitedRoom)
            {
                roomWeights[room] = Mathf.Max(roomWeights[room] * 0.5f, 0.1f); // Reduces the weight of the visited room.
            }
            else
            {
                roomWeights[room] += 0.1f; // Increases the weight of other rooms
            }
        }
    }
}
/*

//OLD CODE:

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
*/