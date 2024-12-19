using UnityEngine;
using UnityEngine.UI; // UI Text component
using System;
using TMPro;

public class FortressHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject GameOverPanel; // Reference to the Game Over Panel 

    // Event to detect health changes
    public event Action OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        OnHealthChanged?.Invoke(); //Initial
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        OnHealthChanged?.Invoke(); //Update to health display notification

        Debug.Log("Fortress Health: " + currentHealth);


        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        Debug.Log("Game Over!");
        Destroy(gameObject);
        // Show Game over panel in canvas
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
    
    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke();
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}


