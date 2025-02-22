using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<Enemy_Base> enemies = new List<Enemy_Base>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Enemy_Base enemy = child.GetComponent<Enemy_Base>();
            if (enemy != null)
            {
                RegisterEnemy(enemy);
            }
        }
    }
    public void RegisterEnemy(Enemy_Base enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy_Base enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
}
