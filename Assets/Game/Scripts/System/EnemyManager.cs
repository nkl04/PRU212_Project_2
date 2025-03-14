using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemyList;

    private void Awake()
    {
        enemyList = new List<GameObject>();
    }
    public void RegisterEnemyGameObject(GameObject enemyGameObject)
    {
        enemyList.Add(enemyGameObject);
    }
    public void UnregisterEnemyGameObject(GameObject enemyGameObject)
    {
        enemyList.Remove(enemyGameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            Enemy_Base enemyBase = enemy.GetComponent<Enemy_Base>();
            if (enemyBase != null)
            {
                enemyBase.StateMachine.ChangeState(enemyBase.EnemyStateDie);
            }
        }
        enemyList.Clear();
    }

    public bool IsClearEnemies()
    {
        return enemyList.Count == 0;
    }
}
