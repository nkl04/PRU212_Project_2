using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : Spawner
{
    private Camera theCamera;
    private Vector2 spawnCenter;
    private Queue<SpawnTask> spawnQueue = new Queue<SpawnTask>();
    private float lastSpawnTime;
    private EnemyManager enemyManager;
    private void Start()
    {
        theCamera = Camera.main;
        spawnCenter = theCamera != null ? theCamera.transform.position : Vector2.zero;
        enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    private void Update()
    {
        if (theCamera != null && (Vector2)theCamera.transform.position != spawnCenter)
        {
            spawnCenter = theCamera.transform.position;
        }

        ProcessSpawnQueue();
    }

    public override void SetWave(Wave wave)
    {
        foreach (var waveEnemy in wave.waveEnemyList)
        {
            if (wave.spawnStyle == SpawnStyle.Immediately)
            {
                AddEnemiesToQueue(waveEnemy, wave.radiousSpawnCircle, wave.offset);
            }
            else if (wave.spawnStyle == SpawnStyle.Sequentially)
            {
                StartCoroutine(SpawnSequentially(waveEnemy, wave.radiousSpawnCircle, wave.offset, wave.startTime, wave.endTime, wave.timeBetweenSpawns));
            }
        }
    }

    private void AddEnemiesToQueue(WaveEnemy waveEnemy, float radius, float offset)
    {
        for (int i = 0; i < waveEnemy.enemyAmount; i++)
        {
            Vector2 spawnPos = GetRandomSpawnPosition(radius, offset);
            spawnQueue.Enqueue(new SpawnTask(waveEnemy.enemyPrefab.name, spawnPos));
        }
    }

    private IEnumerator SpawnSequentially(WaveEnemy waveEnemy, float radius, float offset, float startTime, float endTime, float timeBetweenSpawns)
    {
        for (float spawnTime = startTime; spawnTime <= endTime; spawnTime += timeBetweenSpawns)
        {
            spawnQueue.Enqueue(new SpawnTask(waveEnemy.enemyPrefab.name, GetRandomSpawnPosition(radius, offset)));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void ProcessSpawnQueue()
    {
        if (spawnQueue.Count == 0 || Time.time - lastSpawnTime < 0.1f) return;

        var task = spawnQueue.Dequeue();
        var enemy = ObjectPooler.Instance.GetObjectFromPool(task.enemyPrefabName);

        if (enemy != null)
        {
            enemy.transform.position = task.spawnPosition;
            enemy.SetActive(true);
            lastSpawnTime = Time.time;
            enemyManager.RegisterEnemy(enemy.GetComponent<Enemy_Base>());
        }
    }

    private Vector2 GetRandomSpawnPosition(float radius, float offset)
    {
        float minRadius = radius - offset;
        float maxRadius = radius + offset;
        Vector2 spawnPos;

        do
        {
            spawnPos = spawnCenter + Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
        } while (Vector2.Distance(spawnCenter, spawnPos) < minRadius);

        return spawnPos;
    }

    private class SpawnTask
    {
        public string enemyPrefabName;
        public Vector2 spawnPosition;

        public SpawnTask(string enemyPrefabName, Vector2 spawnPosition)
        {
            this.enemyPrefabName = enemyPrefabName;
            this.spawnPosition = spawnPosition;
        }
    }
}
