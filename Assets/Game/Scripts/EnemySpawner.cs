using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : Spawner
{
    private Camera theCamera;
    private void Start()
    {
        if (!theCamera)
        {
            theCamera = Camera.main;
        }
    }

    public override void Spawn(Wave wave)
    {
        Debug.Log($"Spawning wave at {wave.startTime}s");
        float radius = theCamera.orthographicSize + 3;
        Vector2 spawnCenter = theCamera.transform.position;

        foreach (WaveEnemy waveEnemy in wave.waveEnemyList)
        {
            StartCoroutine(SpawnEnemies(waveEnemy, radius, spawnCenter));
        }
    }

    private IEnumerator SpawnEnemies(WaveEnemy waveEnemy, float radius, Vector2 spawnCenter)
    {
        for (int i = 0; i < waveEnemy.enemyAmount; i++)
        {
            var enemy = ObjectPooler.Instance.GetObjectFromPool(waveEnemy.enemyPrefab.name);
            enemy.transform.position = spawnCenter + Random.insideUnitCircle.normalized * radius;
            enemy.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
