using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the enemy
    private float currentHealth;

    public GameObject healthBarPrefab; // Reference to the health bar prefab
    private HealthBar healthBar; // Reference to the health bar instance

    // Reference to the player's stats
    public PlayerStats playerStats;

    void Start()
    {
        currentHealth = maxHealth;  // Set the enemy's health to maximum at start

        // Instantiate the health bar and attach it to the enemy
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthBarInstance.transform.localPosition = new Vector3(0, 2, 0); // Adjust position above the enemy

        healthBar = healthBarInstance.GetComponentInChildren<HealthBar>();
    }

    // Method to reduce enemy health
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, maxHealth); // Update health bar

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Destroy the enemy when health reaches 0
    void Die()
    {
        if (playerStats != null)
        {
            playerStats.AddSkillPoints(2);
        }

        Destroy(gameObject);
    }
}
