using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the enemy
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;  // Set the enemy's health to maximum at start
    }

    // Method to reduce enemy health
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Destroy the enemy when health reaches 0
    void Die()
    {
        Destroy(gameObject);
    }
}
