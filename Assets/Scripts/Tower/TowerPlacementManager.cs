using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    public GameObject towerPrefab;  // The tower prefab to place
    public PlayerStats playerStats; // Reference to the PlayerStats script
    public int maxTowers = 8;  // Maximum number of towers allowed
    private int currentTowerCount = 0; // Current number of towers placed

    void Update()
    {
        // Example tower placement with left mouse click
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            PlaceTower();
        }
    }

    void PlaceTower()
    {
        // Check if the tower limit is reached before spending a skill point
        if (currentTowerCount >= maxTowers)
        {
            Debug.Log("Maximum number of towers reached. Cannot place more towers.");
            return;
        }

        // Check if the player has enough skill points
        if (playerStats.SpendSkillPoint())
        {
            // Raycast to determine where to place the tower
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Instantiate the tower at the hit point
                Instantiate(towerPrefab, hit.point, Quaternion.identity);
                currentTowerCount++; // Increase the tower count
                Debug.Log("Tower placed successfully. Total towers: " + currentTowerCount);
            }
        }
        else
        {
            Debug.Log("Cannot place tower: not enough skill points.");
        }
    }
}
