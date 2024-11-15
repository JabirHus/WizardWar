using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int skillPoints = 2;  // The player starts with 2 skill points

    public event Action OnSkillPointsChanged;

    void Start()
    {
        // Initial notification of the skill points
        OnSkillPointsChanged?.Invoke();
    }

    public bool SpendSkillPoint()
    {
        if (skillPoints > 0)
        {
            skillPoints=skillPoints-2;
            OnSkillPointsChanged?.Invoke();
            Debug.Log("Skill point spent. Remaining skill points: " + skillPoints);
            return true;
        }
        else
        {
            Debug.Log("No skill points remaining.");
            return false;
        }
    }

    public void AddSkillPoints(int amount)
    {
        skillPoints += amount;
        OnSkillPointsChanged?.Invoke();
        Debug.Log(amount + " skill points added. Total skill points: " + skillPoints);
    }
}

