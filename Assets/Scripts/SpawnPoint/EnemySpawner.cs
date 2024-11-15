using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array for different enemies 
    public Transform[] spawnPoints; // Array of spawn points
    public int maxWaves = 7; // Total number of waves
    public int enemiesPerWave = 5; 
    public float timeBetweenWaves = 7f; // Delay between waves
    public float spawnInterval = 3f; // Time between enemy spawns in a wave

    private int currentWave = 1; 
    private int enemiesSpawnedInWave = 0; 

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (currentWave <= maxWaves)
        {
            Debug.Log("Starting Wave: " + currentWave);

            enemiesSpawnedInWave = 0;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                enemiesSpawnedInWave++;
                yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
            }

            Debug.Log("Wave " + currentWave + " complete!");
            currentWave++;
            enemiesPerWave += 2; // Increase difficulty by spawning more enemies in subsequent waves
            yield return new WaitForSeconds(timeBetweenWaves); // Wait before the next wave starts
        }

        Debug.Log("All waves complete!");
    }

    private void SpawnEnemy()
    {
        // Chooses Area in spawner
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Randomize enemy type
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);
    }
}
