using TMPro;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private float waveTime = 5000;
    [SerializeField] private int waveEnemies = 10;
    [SerializeField] private Camera theCamera;
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

    public override void Spawn()
    {
        if (prefab)
        {
            var radius = theCamera.orthographicSize + 3;
            var p = theCamera.transform.position;
            var numEnemies = waveEnemies + _waveNumber;

            for (var i = 0; i < numEnemies; i++)
            {
                var enemy = ObjectPooler.Instance.GetObjectFromPool(prefab.name);
                enemy.transform.position = new Vector2(p.x, p.y) + Random.insideUnitCircle.normalized * radius;
                enemy.SetActive(true);
            }
            _waveNumber++;
        }

        _lastWaveAt = Utilities.CurrentMillis();
    }
}
