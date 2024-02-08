using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpawnManager : MonoBehaviour
{

    public GameObject weapon;
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public GameObject groundItemPrefab;
    public Transform[] groundItemSpawnPoints;
    private List<GameObject> spawnedGroundItems = new List<GameObject>();

    private bool isActive = false;
    public GameObject screen_shake;

    public void ActivateRoom()
    {
        Debug.Log(isActive);

        if (!isActive)
        {
            SpawnEnemies();
            SpawnGroundItems();
            isActive = true;
            Debug.Log("Room Activated");
        }
    }

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

    private void SpawnGroundItems()
    {
        Debug.Log("Spawning ground items...");
        foreach (Transform spawnPoint in groundItemSpawnPoints)
        {
            GameObject groundItem = Instantiate(groundItemPrefab, spawnPoint.position, Quaternion.identity);
            if (Random.Range(0, 100) <= 50)
            {
                groundItem.GetComponent<GroundItem>().ThisItemID = 1;
            }
            else
            {
                groundItem.GetComponent<GroundItem>().ThisItemID = 2;
            }
            spawnedGroundItems.Add(groundItem);
            Debug.Log("Spawned ground item at " + spawnPoint.position);
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

    public void ResetRoom()
    {
        isActive = false;
        Debug.Log("Room reset");
    }

}