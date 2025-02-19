using System;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount = 0;

    private void Start()
    {
        killCountText.text = killCount.ToString();
        EventHandlers.OnEnemyDeadEvent += OnEnemyDead;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        killCountText.text = (++killCount).ToString();
    }
}
