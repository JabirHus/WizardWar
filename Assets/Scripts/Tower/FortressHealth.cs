using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // UI TO BE IMPLEMENTED
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
        // Show Game over/restart screen
    }
}