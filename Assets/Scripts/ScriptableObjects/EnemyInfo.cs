using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Info", menuName = "Scriptable Objects/Enemy Info")]
public class EnemyInfo : EntityInfo
{
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
