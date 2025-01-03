using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int skillPoints = 100; // Starting skill points
    public event Action OnSkillPointsChanged;
    public TextMeshProUGUI text;

    void Start()
    {
        // Notify listeners of the initial skill points
        text.enabled = false;
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
            StartCoroutine(disableText());
            Debug.Log($"Not enough skill points! Required: {cost}, Available: {skillPoints}");
            return false;
        }
    }

    public IEnumerator disableText()
    {
        text.enabled = true;
        yield return new WaitForSeconds(1.5f);
        text.enabled = false;
    }

    public void AddSkillPoints(int amount)
    {
        skillPoints += amount;
        OnSkillPointsChanged?.Invoke();
        Debug.Log($"{amount} skill points added. Total skill points: {skillPoints}");
    }
}
