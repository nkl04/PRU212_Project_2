using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy_Base> enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy_Base>();
    }

    private void Update()
    {
        foreach (var enemy in enemyList)
        {
            enemy.StateMachine.Update();
            CheckCollision(enemy);
        }
    }

    public void RegisterEnemy(Enemy_Base enemy)
    {
        enemyList.Add(enemy);
        enemy.Initialize(enemyList.Count);
    }
    public void UnregisterEnemy(Enemy_Base enemy)
    {
        enemyList.Remove(enemy);
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

    public void CheckCollision(Enemy_Base enemy)
    {
        Collider2D enemyCollider = enemy.Collider2D;
        Collider2D playerCollider = enemy.Target.GetComponent<Collider2D>();

        bool isColliding = enemyCollider.IsTouching(playerCollider);

        if (isColliding)
        {
            enemy.CanFollowPlayer = false;
            enemy.CanAttackPlayer = true;
        }
        else
        {
            enemy.CanFollowPlayer = true;
            enemy.CanAttackPlayer = false;
        }
    }
}
