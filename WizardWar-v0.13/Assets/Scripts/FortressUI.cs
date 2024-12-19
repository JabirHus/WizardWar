using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FortressUI : MonoBehaviour
{
    public FortressHealth MainTower;
    public TextMeshProUGUI fortressHealthText;

    void Start()
    {
        if (MainTower != null)
        {
            MainTower.OnHealthChanged += UpdateHealthText; // Link to the event
            UpdateHealthText(); // Initial update of the health display
        }
        else
        {
            Debug.LogError("MainTower is not assigned in FortressUI!");
        }
    }

  
    void OnDestroy()
    {
        if (MainTower != null)
        {
            MainTower.OnHealthChanged -= UpdateHealthText; // Unlink to event
        }
    }


    void UpdateHealthText()
    {
        if (fortressHealthText != null)
        {
            fortressHealthText.text = "Fortress Health: " + MainTower.GetCurrentHealth();
        }
        else
        {
            Debug.LogError("FortressHealthText is not assigned in FortressUI!");
        }
    }
}
