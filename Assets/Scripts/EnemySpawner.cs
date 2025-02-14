using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float waveTime = 5000;
    public int waveEnemies = 10;
    public Camera theCamera;
    private float _lastWaveAt;
    private int _waveNumber;

    private void Start()
    {
        if (!theCamera)
        {
            theCamera = Camera.main;
        }

        _lastWaveAt = -waveTime - 1;
    }

    private void Update()
    {
        if (Utilities.CurrentMillis() - _lastWaveAt > waveTime)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (enemyPrefab)
        {
            var radius = theCamera.orthographicSize + 3;
            var p = theCamera.transform.position;
            var numEnemies = waveEnemies + _waveNumber;

            for (var i = 0; i < numEnemies; i++)
            {
                var enemy = ObjectPooler.Instance.GetObjectFromPool(enemyPrefab.name);
                enemy.transform.position = new Vector2(p.x, p.y) + Random.insideUnitCircle.normalized * radius;
                enemy.SetActive(true);
            }
            _waveNumber++;
        }

        _lastWaveAt = Utilities.CurrentMillis();
    }
}
