using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (enemy.CanFollowPlayer)
        {
            stateMachine.ChangeState(new EnemyStateRun(enemy, stateMachine));
        }
        else if (enemy.CanAttackPlayer)
        {
            stateMachine.ChangeState(new EnemyStateAttack(enemy, stateMachine));
        }
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        CheckChangeState();
    }

    public override void Exit()
    {
    }
}
