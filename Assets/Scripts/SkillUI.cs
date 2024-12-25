using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI skillPointsText;

    void Start()
    {
        
        playerStats.OnSkillPointsChanged += UpdateSkillPointsText;
        // Initial update of the skill points display
        UpdateSkillPointsText();
    }

    void OnDestroy()
    {
        
        playerStats.OnSkillPointsChanged -= UpdateSkillPointsText;
    }

    void UpdateSkillPointsText()
    {
        skillPointsText.text = "Skill Points: " + playerStats.skillPoints;
    }
}