//IMPLEMENTED WAYPOINT PATHS
// For spawnpoint 1 - Path 1 = Waypoint1,Waypoint2,Waypoint3           
// For Spawnpoint2 - Path 2 = WaypointA,WaypointB,WaypointC,WaypointD
// Path Spawnpoint3 - path 3 = WaypointG,WaypointF,WaypointE,WaypointD


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WaveManager : MonoBehaviour
{

    // Different paths ( assigned in inspector based on above comments)
    public Transform[] WaypointPath1;
    public Transform[] WaypointPath2;
    public Transform[] WaypointPath3;

    public GameObject[] enemyPrefabs; // Array for different enemies 
    public Transform[] spawnPoints; // Array of spawn points
    public int maxWaves = 4; // Total number of waves
    public int enemiesPerWave = 3; 
    public float timeBetweenWaves = 25f; // Delay between waves
    public float spawnInterval = 2f; // Time between enemy spawns in a wave
    private float waitUntilCameraAnimationCompletes = 5f;

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
        if (currentWave == 1)
        {
            yield return new WaitForSeconds(waitUntilCameraAnimationCompletes);
        }
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

            yield return new WaitForSeconds(timeBetweenWaves); // Wait before the next wave starts

            currentWave++;

            OnWaveNumChanged?.Invoke(); //Update to wave num display
        }

        Debug.Log("YOU WIN - All waves complete!");
    }

    private void SpawnEnemy()
    {
        //Pick spawnpoint randomly
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemy = null; 

        // Enemy type spawned is only infantry on wave 1
        if (currentWave==1){
            enemy = Instantiate(enemyPrefabs[0], spawnPoint.position, spawnPoint.rotation);
        }
        //Enemy type flying occur in wave 3
        if (currentWave==3){
            enemiesPerWave=4;
            enemy = Instantiate(enemyPrefabs[2], spawnPoint.position, spawnPoint.rotation);
        }
        //Spawn final wizard on wave 7
        if (currentWave==4){
            enemiesPerWave=1;
            enemy = Instantiate(enemyPrefabs[3], spawnPoint.position, spawnPoint.rotation);
        }
        else{ 
            int enemyIndex = UnityEngine.Random.Range(0, 2);
            enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        }


        //Assigning path to enemy (in movement script - does not apply to flying) based on spawnpoint
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();  
        if (enemyMovement != null)
        {
            if (spawnIndex == 0 )
            {
                enemyMovement.path = WaypointPath1;
            }
            else if (spawnIndex == 1)
            {
                enemyMovement.path = WaypointPath2;
            }
            else if (spawnIndex == 2)
            {
                enemyMovement.path = WaypointPath3;
            }
        }
    }

    public float GetCurrentWave()
    {
        return currentWave;
    }
}

