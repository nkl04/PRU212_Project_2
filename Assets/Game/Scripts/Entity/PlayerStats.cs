using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    private float exp = 0;
    public float Exp
    {
        get => exp; set
        {
            exp = value;
            EventHandlers.CallOnExpCollectedEvent(exp, expToNextLevel);
            if (exp >= expToNextLevel)
            {
                Level++;
                EventHandlers.CallOnLevelUpEvent(Level);
            }
        }
    }
    private int level = 1;
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    private float expToNextLevel;
    private float expToNextLevelMultiplier = 1;
    private float expToNextLevelBase = 5;
    private void Start()
    {
        EventHandlers.OnLevelUpEvent += UpdateExp;
        UpdateExp(level);
    }
    private void OnDestroy()
    {
        EventHandlers.OnLevelUpEvent -= UpdateExp;
    }

    private void UpdateExp(int level)
    {
        expToNextLevel = expToNextLevelMultiplier * Mathf.Pow(level, 2) - expToNextLevelMultiplier * level + expToNextLevelBase;
        Exp = 0;
    }
}
