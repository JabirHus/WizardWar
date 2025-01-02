using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 250f; // Maximum health of the enemy
    public bool isEvilWizard = false; //Indicates if enemy is evil wizard 
    public GameObject GameOverPanel; // Reference to the Game Over Panel if evil wizard dies
    // Reference to the player's stats
    public PlayerStats playerStats;
    public GameObject healthBarPrefab; // Reference to the health bar prefab
    private HealthBar healthBar; // Reference to the health bar instance
    private float currentHealth;


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

    // Destroy the enemy when health reaches 0 and add skill points
    void Die()
    {
        if (isEvilWizard){
            // Show Game over panel in canvas
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
        if (playerStats != null)
        {
            playerStats.AddSkillPoints(25);
        }

        Destroy(gameObject);
    }
}

