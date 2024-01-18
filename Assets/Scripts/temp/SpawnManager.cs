using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isActive = false;

    public void ActivateRoom()
    {
        if (!isActive)
        {
            SpawnPlayer();
            SpawnEnemies();
            isActive = true;
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    public void DeactivateRoom()
    {
        isActive = false;
        // Additional deactivation logic if needed
    }

    public bool AreAllEnemiesDefeated()
    {
        spawnedEnemies.RemoveAll(item => item == null);
        return spawnedEnemies.Count == 0;
    }
}
/*
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;
    public bool isActive = false; // Indicates if the room is active

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isActive)
        {
            SpawnPlayer();
            SpawnEnemies();
            isActive = true;
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    // Call this method to deactivate the room
    public void DeactivateRoom()
    {
        isActive = false;
        // Additional logic for room deactivation, if necessary
    }

    public bool AreAllEnemiesDefeated()
    {
        spawnedEnemies.RemoveAll(item => item == null); // Remove any null references (destroyed enemies)
        return spawnedEnemies.Count == 0; // If no enemies remain, return true
    }
}



{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
*/