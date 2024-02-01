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