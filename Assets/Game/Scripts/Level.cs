using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ConfigLevel _configLevel;
    [Space(10)]
    [SerializeField] private Transform _backGround;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        enemySpawner = GetComponentInChildren<EnemySpawner>();
    }
    private void Start()
    {
        _backGround.GetComponent<SpriteRenderer>().sprite = _configLevel.background;
    }
}
