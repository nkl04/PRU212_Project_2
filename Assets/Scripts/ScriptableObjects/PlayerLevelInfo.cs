using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelInfo", menuName = "Scriptable Objects/PlayerLevelInfo", order = 1)]
public class PlayerLevelInfo : ScriptableObject
{
    public int level;
    public int experience;
}


