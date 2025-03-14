using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    public float Exp
    {
        get => exp; set
        {
            exp = value;
            EventHandlers.CallOnExpCollectedEvent(exp, expToNextLevel, this.transform);
            if (exp >= expToNextLevel)
            {
                Level++;
                EventHandlers.CallOnLevelUpEvent(Level);
            }
        }
    }
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    public float MaxHealth { get; set; }
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }
    public float DamageReduction { get; set; } = 0;

    private float exp = 0;
    private int level = 1;
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
