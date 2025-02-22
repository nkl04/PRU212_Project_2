using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config Enemy", menuName = "Scriptable Objects/Config Enemy")]
public class ConfigEnemy : ConfigEntity
{
    public float _baseAttackRate;

    [Header("Rewards")]
    public List<Reward> RewardList;
}

[Serializable]
public class Reward
{
    public GameObject RewardPrefab;
    public int Amount;
    public float DropRate;
}
