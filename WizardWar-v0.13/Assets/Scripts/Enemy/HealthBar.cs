using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFillImage; // Reference to the fill image of the health bar

    // Method to update the health bar
    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
    }
}
