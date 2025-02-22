using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public List<WaveEnemy> list;
    public int enemyAmount;
}

public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int enemyAmount;
}



