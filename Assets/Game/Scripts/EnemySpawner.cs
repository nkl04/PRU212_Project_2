using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    private Camera theCamera;
    private Vector2 spawnCenter;
    private Queue<SpawnTask> spawnQueue = new Queue<SpawnTask>();
    private float lastSpawnTime = 0f;

    private void Start()
    {
        theCamera = Camera.main;
    }

    private void Update()
    {
        if (theCamera != null)
        {
            spawnCenter = theCamera.transform.position;
        }

        ProcessSpawnQueue();
    }

    public override void Spawn(Wave wave)
    {
        float radius = wave.radiousSpawnCircle;
        float offset = wave.offset;

        foreach (WaveEnemy waveEnemy in wave.waveEnemyList)
        {
            if (wave.spawnStyle == SpawnStyle.Immediately)
            {
                AddEnemiesToQueue(waveEnemy, radius, offset, 0);
            }
            else
            {
                AddEnemiesToQueue(waveEnemy, radius, offset, wave.timeBetweenSpawns);
            }
        }
    }

    private void AddEnemiesToQueue(WaveEnemy waveEnemy, float radius, float offset, float timeBetweenSpawns)
    {
        float minRadius = radius - offset;
        float maxRadius = radius + offset;

        for (int i = 0; i < waveEnemy.enemyAmount; i++)
        {
            Vector2 spawnPos;
            do
            {
                spawnPos = spawnCenter + Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            }
            while (Vector2.Distance(spawnCenter, spawnPos) < minRadius);

            spawnQueue.Enqueue(new SpawnTask(waveEnemy.enemyPrefab.name, spawnPos, timeBetweenSpawns));
        }
    }

    private void ProcessSpawnQueue()
    {
        if (spawnQueue.Count == 0) return;

        SpawnTask task = spawnQueue.Peek();
        if (Time.time - lastSpawnTime >= task.timeBetweenSpawns)
        {
            var enemy = ObjectPooler.Instance.GetObjectFromPool(task.enemyPrefabName);
            if (enemy != null)
            {
                enemy.transform.position = task.spawnPosition;
                enemy.SetActive(true);
            }
            lastSpawnTime = Time.time;
            spawnQueue.Dequeue();
        }
    }

    private class SpawnTask
    {
        public string enemyPrefabName;
        public Vector2 spawnPosition;
        public float timeBetweenSpawns;

        public SpawnTask(string enemyPrefabName, Vector2 spawnPosition, float timeBetweenSpawns)
        {
            this.enemyPrefabName = enemyPrefabName;
            this.spawnPosition = spawnPosition;
            this.timeBetweenSpawns = timeBetweenSpawns;
        }
    }
}
