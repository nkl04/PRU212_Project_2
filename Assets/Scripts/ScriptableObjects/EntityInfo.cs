using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : ScriptableObject
{
    [Header("Base Info")]
    public string _name;
    public float _baseMaxHealth;
    public float _baseDamage;
    public float _baseAttackRate;
    public float _baseSpeed;

    [Header("Rewards")]
    public List<Reward> RewardList;
}
