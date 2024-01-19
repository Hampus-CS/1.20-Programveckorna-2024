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
    }

    public bool AreAllEnemiesDefeated()
    {
        spawnedEnemies.RemoveAll(item => item == null);
        return spawnedEnemies.Count == 0;
    }
}