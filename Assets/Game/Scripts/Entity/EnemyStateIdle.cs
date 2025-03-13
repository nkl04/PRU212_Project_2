using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (enemy.CanFollowPlayer && Vector2.Distance(enemy.transform.position, enemy.Target.transform.position) < 30f)
        {
            stateMachine.ChangeState(enemy.EnemyStateRun);
        }
        else if (enemy.CanAttackPlayer)
        {
            stateMachine.ChangeState(enemy.EnemyStateAttack);
        }
        else if (Vector2.Distance(enemy.transform.position, enemy.Target.transform.position) >= 60f)
        {
            stateMachine.ChangeState(enemy.EnemyStateDie);
        }
    }

    public override void Enter()
    {
        enemy.Collider2D.enabled = true;
    }

    public override void Execute()
    {
        CheckChangeState();
    }

    public override void Exit()
    {
    }
}
