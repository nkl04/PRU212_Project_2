using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config Enemy", menuName = "Scriptable Objects/Config Enemy")]
public class ConfigEnemy : ConfigEntity
{
    public float _baseAttackRate;

    public ConfigReward configReward;
}

