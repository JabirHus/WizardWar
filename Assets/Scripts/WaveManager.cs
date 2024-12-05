using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array for different enemies 
    public Transform[] spawnPoints; // Array of spawn points
    public int maxWaves = 4; // Total number of waves
    public int enemiesPerWave = 3; 
    public float timeBetweenWaves = 17f; // Delay between waves
    public float spawnInterval = 4f; // Time between enemy spawns in a wave

    private int currentWave = 1; 
    private int enemiesSpawnedInWave = 0; 

    // Event to detect wave number so it can be displayed
    public event Action OnWaveNumChanged;

    void Start()
    {
        StartCoroutine(SpawnWave());
        
        OnWaveNumChanged?.Invoke(); //initial wave number display

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

            OnWaveNumChanged?.Invoke(); //Update to wave num display

            yield return new WaitForSeconds(timeBetweenWaves); // Wait before the next wave starts
        }

        Debug.Log("YOU WIN - All waves complete!");
    }

    private void SpawnEnemy()
    {
        //Pick spawnpoint randomly
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Enemy type spawned is only infantry on wave 1
        if (currentWave==1){
            Instantiate(enemyPrefabs[0], spawnPoint.position, spawnPoint.rotation);
        }
        //Enemy type is flying in final wave
        if (currentWave==3){
            enemiesPerWave=2;
            Instantiate(enemyPrefabs[2], spawnPoint.position, spawnPoint.rotation);
        }
        //Spawn final wizard on wave 7
        if (currentWave==4){
            enemiesPerWave=1;
            Instantiate(enemyPrefabs[3], spawnPoint.position, spawnPoint.rotation);
        }
        else{ 
            int enemyIndex = UnityEngine.Random.Range(0, 2);
            Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }

    public float GetCurrentWave()
    {
        return currentWave;
    }
}
