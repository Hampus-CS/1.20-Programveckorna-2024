using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Enemy = EnemyMovement;


/*
 * OLD
public class Room : MonoBehaviour
{
    public List<Transform> enemySpawnPoints;
    public int timesVisited = 0;
    public GameObject EnemyPrefab;

    public void SpawnEnemies()
    {
        if (EnemyPrefab == null)
        {
            Debug.LogError("EnemyPrefab is not assigned. Please assign an enemy prefab in the Inspector.");
            return;
        }

        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public bool AllEnemiesDefeated()
    {
        // Make sure the 'Enemy' class is defined in the appropriate namespace.
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(spawnPoint.position, enemy.transform.position) < 1f)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
*/