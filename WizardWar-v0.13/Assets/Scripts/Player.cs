using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int skillPoints = 100; // Starting skill points
    public event Action OnSkillPointsChanged;

    void Start()
    {
        // Notify listeners of the initial skill points
        OnSkillPointsChanged?.Invoke();
    }

    public bool SpendSkillPoints(int cost)
    {
        if (skillPoints >= cost)
        {
            skillPoints -= cost;
            OnSkillPointsChanged?.Invoke();
            Debug.Log($"Spent {cost} skill points. Remaining skill points: {skillPoints}");
            return true;
        }
        else
        {
            Debug.Log($"Not enough skill points! Required: {cost}, Available: {skillPoints}");
            return false;
        }
    }

    public void AddSkillPoints(int amount)
    {
        skillPoints += amount;
        OnSkillPointsChanged?.Invoke();
        Debug.Log($"{amount} skill points added. Total skill points: {skillPoints}");
    }
}
