using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float waveTime = 5000;
    public int waveEnemies = 10;
    public Camera theCamera;
    public Player player;

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

            // inc old enemies speed
            var oldEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var oldEnemy in oldEnemies)
            {
                var enemyCtrl = oldEnemy.GetComponent<Enemy>();
                var r = Random.Range(0, 10);
                switch (r)
                {
                    case < 5:
                        enemyCtrl.speed += Random.Range(0.1f, 1.0f);
                        enemyCtrl.speed = Mathf.Max(enemyCtrl.speed, 6);
                        break;
                    case > 8:
                        enemyCtrl.hp += Random.Range(1, _waveNumber / 2);
                        break;
                }
            }

            for (var i = 0; i < numEnemies; i++)
            {
                var enemy = ObjectPooler.Instance.GetObjectFromPool(enemyPrefab.name);
                enemy.transform.position = new Vector2(p.x, p.y) + Random.insideUnitCircle.normalized * radius;
                enemy.SetActive(true);
            }

            // // inc player DPS
            // if (_waveNumber % 5 == 0 && _waveNumber > 0)
            // {
            //     if (player)
            //     {
            //         player.dps = Mathf.Min(10, player.dps + 1);
            //         if (_waveNumber % 10 == 0)
            //         {
            //             player.bullets = Mathf.Min(player.bullets + 1, 20);
            //         }
            //     }
            // }

            _waveNumber++;
        }

        _lastWaveAt = Utilities.CurrentMillis();
    }
}
