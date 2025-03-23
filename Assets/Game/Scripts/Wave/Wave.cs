using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [Tooltip("Time to start the wave (seconds)")]
    public int startTime;
    public int endTime;
    [Tooltip("Radious of the circle where the enemies will spawn")]
    public float radiousSpawnCircle;
    public float offset;
    public SpawnStyle spawnStyle;
    public float timeBetweenSpawns;
    public List<WaveEnemy> waveEnemyList;
}

[Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int enemyAmount;
}

public enum SpawnStyle
{
    Sequentially,
    Immediately
}


