using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy1; // Reference to the enemy prefab
    public Transform[] spawnPoints; // Array of spawn points

    private int maxEnemies = 5; // Maximum number of enemies to spawn
    private int currentEnemyCount = 0; // Current number of active enemies

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); // Adjust the wait time if needed
        }
    }

    private void SpawnEnemy()
    {
        // Choose a point in spawner
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the enemy prefab at the chosen spawn point
        Instantiate(Enemy1, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++; // Increment the count of active enemies
    }

}