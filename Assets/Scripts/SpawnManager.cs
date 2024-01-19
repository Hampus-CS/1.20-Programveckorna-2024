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
    public GameObject screen_shake;

    public void ActivateRoom()
    {
        Debug.Log(isActive);

        if (!isActive)
        {
            // SpawnPlayer(); // Remove this line
            SpawnEnemies();
            isActive = true;
            Debug.Log("Room Activated");
        }
    }

    /*
    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
    */

    private void SpawnEnemies()
    {
        Debug.Log("Spawning enemies...");
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().screen_shake = screen_shake;
            spawnedEnemies.Add(enemy);
            Debug.Log("Spawned enemy at " + spawnPoint.position);
        }
    }

    public void DeactivateRoom()
    {
        isActive = false;
        Debug.Log("DeactivateRoom");
    }

    public bool AreAllEnemiesDefeated()
    {
        Debug.Log("AreAllEnemiesDefeated");
        spawnedEnemies.RemoveAll(item => item == null);
        return spawnedEnemies.Count == 0;
    }
}