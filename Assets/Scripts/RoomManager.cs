using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private List<int> roomWeights = new List<int>();
    private int lastRoomIndex = -1;
    private const int START_ROOM_INDEX = 0;
    private Transform playerTransform;

    public Transform[] roomSpawnPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeRoomWeights();
        Debug.Log("InitializeRoomWeight");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void InitializeRoomWeights()
    {
        for (int i = 0; i < 12; i++)
        {
            roomWeights.Add(1); // Initialize all weights to 1
        }

        // Set starting room weight to 0 after first teleport
        roomWeights[START_ROOM_INDEX] = 0;
    }

    public int GetNextRoomIndex()
    {
        int totalWeight = 0;
        foreach (int weight in roomWeights)
            totalWeight += weight;

        int randomNumber = Random.Range(0, totalWeight);
        int roomIndex = 0;

        foreach (int weight in roomWeights)
        {
            if (randomNumber < weight)
                break;
            randomNumber -= weight;
            roomIndex++;
        }

        // Update weights
        for (int i = 0; i < roomWeights.Count; i++)
        {
            if (i == roomIndex)
                roomWeights[i] = 0;
            else
                roomWeights[i]++;
        }

        lastRoomIndex = roomIndex;
        return roomIndex;
    }

    public void TeleportPlayer(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < roomSpawnPoints.Length)
        {
            playerTransform.position = roomSpawnPoints[roomIndex].position;
            Debug.Log("Player teleported to room index " + roomIndex);

            // Optionally, you can also activate the room here
            SpawnManager spawnManager = roomSpawnPoints[roomIndex].GetComponent<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.ActivateRoom();
            }
            else
            {
                Debug.LogError("No SpawnManager found in room " + roomIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid room index for teleportation.");
        }
    }
}