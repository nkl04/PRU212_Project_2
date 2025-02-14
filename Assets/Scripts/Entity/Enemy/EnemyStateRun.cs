using UnityEngine;

public class EnemyStateRun : EnemyState
{
    public EnemyStateRun(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (!enemy.CanFollowPlayer)
        {
            stateMachine.ChangeState(new EnemyStateIdle(enemy, stateMachine));
        }
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        enemy.MoveTowardsTarget();
        CheckChangeState();
    }

    public override void Exit()
    {
    }
}
