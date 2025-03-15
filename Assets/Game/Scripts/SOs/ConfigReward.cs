using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config Reward", menuName = "Scriptable Objects/Config Reward")]
public class ConfigReward : ScriptableObject
{
    public List<Reward> RewardList;
}

[Serializable]
public class Reward
{
    public GameObject RewardPrefab;
    public int Amount;
    public float DropRate;
    public bool HasAnimation = true;
}
