using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemiesPerRoom = 3;
    private Dictionary<Transform, List<GameObject>> spawnedEnemies = new Dictionary<Transform, List<GameObject>>();

    void Start()
    {
        RoomTeleporter.OnRoomTeleport += HandleRoomTeleport;
    }

    void OnDestroy()
    {
        RoomTeleporter.OnRoomTeleport -= HandleRoomTeleport;
    }

    private void HandleRoomTeleport(Transform room)
    {
        ClearEnemies();
        SpawnEnemiesInRoom(room);
    }

    private void SpawnEnemiesInRoom(Transform room)
    {
        if (!spawnedEnemies.ContainsKey(room))
        {
            spawnedEnemies[room] = new List<GameObject>();
        }

        for (int i = 0; i < maxEnemiesPerRoom; i++)
        {
            Vector3 spawnPosition = room.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies[room].Add(enemy);
        }
    }

    private void ClearEnemies()
    {
        foreach (var roomEnemies in spawnedEnemies.Values)
        {
            foreach (var enemy in roomEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
            roomEnemies.Clear();
        }
    }
}