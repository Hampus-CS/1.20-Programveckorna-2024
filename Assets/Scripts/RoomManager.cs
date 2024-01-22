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
    public int currentRoomIndex = -1;
    private Transform playerTransform;
    private CameraController cameraController;

    public Transform[] roomSpawnPoints;
    public GameObject[] cameras;

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void InitializeRoomWeights()
    {
        for (int i = 0; i < roomSpawnPoints.Length; i++)
        {
            roomWeights.Add(1); // Initialize all weights to 1
        }
    }

    public int GetNextRoomIndex()
    {
        int totalWeight = 0; // Initialize totalWeight to 0
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

        currentRoomIndex = roomIndex; // Update the current room index
        return roomIndex;
    }

    public void TeleportPlayer(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < roomSpawnPoints.Length)
        {
            // Debug information
            Debug.Log($"Teleporting player to room {roomIndex}");

            // Disable the previous camera, useless as far as i know.
            if (currentRoomIndex >= 0 && currentRoomIndex < cameras.Length)
            {
                cameras[currentRoomIndex].SetActive(false);
                Debug.Log($"Camera for room {currentRoomIndex} disabled.");
            }

            // Enable the current camera
            if (roomIndex >= 0 && roomIndex < cameras.Length)
            {
                cameras[roomIndex].SetActive(true);
                Debug.Log($"Camera for room {roomIndex} enabled.");
            }

            // Teleport the player to the new room
            playerTransform.position = roomSpawnPoints[roomIndex].position;

            currentRoomIndex = roomIndex; // Update the current room index
        }
        else
        {
            Debug.LogError($"Invalid room index for teleportation: {roomIndex}");
        }
    }
}