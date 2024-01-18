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
    private Transform playerTransform; // Reference to the player's transform

    // Add a list or array to reference room spawn points
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

        // Find and store the player's transform at start
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

    // Call this method to teleport the player
    public void TeleportPlayer(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < roomSpawnPoints.Length)
        {
            playerTransform.position = roomSpawnPoints[roomIndex].position;
        }
        else
        {
            Debug.LogError("Invalid room index for teleportation.");
        }
    }
}

/*
 *  OLD
public class RoomManager : MonoBehaviour
{
    public List<Room> rooms;
    public Transform playerSpawnPoint;
    public int startRoomIndex = 0; // Set a default value

    private int currentRoomIndex = -1;

    public int CurrentRoomIndex
    {
        get { return currentRoomIndex; }
        set { currentRoomIndex = value; }
    }

    private bool enemiesSpawned = false;

    private void Start()
    {
        Debug.Log("Number of rooms: " + rooms.Count);

        // Ensure that roomManager is not null before accessing startRoomIndex.
        if (startRoomIndex >= 0 && startRoomIndex < rooms.Count)
        {
            currentRoomIndex = startRoomIndex; // Set the start room index
            Debug.Log("startRoomIndex: " + startRoomIndex);
            SwitchRoom();
        }
        else
        {
            Debug.LogError("Invalid startRoomIndex in RoomManager!");
        }
    }

    public void SwitchRoom()
    {
        if (currentRoomIndex >= 0 && currentRoomIndex < rooms.Count)
        {
            rooms[currentRoomIndex].gameObject.SetActive(false);
        }

        currentRoomIndex = GetNextRoomIndex();

        Debug.Log("Switching to Room Index: " + currentRoomIndex);

        // Ensure that the new currentRoomIndex is within the valid range
        if (currentRoomIndex >= 0 && currentRoomIndex < rooms.Count)
        {
            rooms[currentRoomIndex].gameObject.SetActive(true);

            // Reset the enemiesSpawned flag for the new room
            enemiesSpawned = false;

            if (!enemiesSpawned)
            {
                rooms[currentRoomIndex].SpawnEnemies();
                enemiesSpawned = true;
            }
        }
        else
        {
            Debug.LogError("Invalid currentRoomIndex after SwitchRoom!");
        }
    }

    private int GetNextRoomIndex()
    {
        // Check if there are any rooms available
        if (rooms.Count == 0)
        {
            Debug.LogError("No rooms available!");
            return 0; // or another appropriate fallback value
        }

        // Calculate the weighted probability of each room excluding the start room
        List<float> weights = new List<float>();
        float totalWeight = 0f;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i != startRoomIndex) // Exclude the start room
            {
                float weight = Mathf.Pow(1.2f, -rooms[i].timesVisited);
                weights.Add(weight);
                totalWeight += weight;

                // Debug log for weight calculation
                Debug.Log($"Room {i} - Weight: {weights[i]}");
            }
        }

        // Randomly select a room based on the weights
        float randomValue = Random.Range(0f, totalWeight);

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i != startRoomIndex && randomValue < weights[i])
            {
                int nextRoomIndex = i;

                // Adjust the currentRoomIndex to skip the start room
                if (nextRoomIndex >= startRoomIndex)
                {
                    nextRoomIndex++;
                }

                // Debug log before returning the final index
                Debug.Log($"Final Room Index: {nextRoomIndex}");

                return nextRoomIndex;
            }

            if (i != startRoomIndex)
            {
                randomValue -= weights[i];
            }
        }

        // If all else fails, return the last room index
        Debug.LogWarning("Fallback to the last room index: " + (rooms.Count - 1));
        return rooms.Count - 1;
    }
}
*/