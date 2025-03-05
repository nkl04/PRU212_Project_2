using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : Spawner
{
    private Camera theCamera;
    private Vector2 spawnCenter;
    private void Start()
    {
        if (!theCamera)
        {
            theCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (theCamera != null)
        {
            spawnCenter = theCamera.transform.position;
        }
    }

    public override void Spawn(Wave wave)
    {
        StartCoroutine(SpawnWave(wave));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        float radius = wave.radiousSpawnCircle;
        float offset = wave.offset;

        if (wave.spawnStyle == SpawnStyle.Immediately)
        {
            foreach (WaveEnemy waveEnemy in wave.waveEnemyList)
            {
                StartCoroutine(SpawnImmediatelyEnemies(waveEnemy, radius, offset, 0));
            }
        }
        else
        {
            foreach (WaveEnemy waveEnemy in wave.waveEnemyList)
            {
                StartCoroutine(SpawnSequentlyEnemies(waveEnemy, radius, offset, wave.timeBetweenSpawns));
            }
        }

        yield return null;
    }

    private IEnumerator SpawnImmediatelyEnemies(WaveEnemy waveEnemy, float radius, float offset, float timeBetweenSpawns)
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

            var enemy = ObjectPooler.Instance.GetObjectFromPool(waveEnemy.enemyPrefab.name);
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    private IEnumerator SpawnSequentlyEnemies(WaveEnemy waveEnemy, float radius, float offset, float timeBetweenSpawns)
    {
        float minRadius = radius - offset;
        float maxRadius = radius + offset;

        Vector2 spawnPos;
        while (true)
        {
            spawnPos = spawnCenter + Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);

            var enemy = ObjectPooler.Instance.GetObjectFromPool(waveEnemy.enemyPrefab.name);
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
