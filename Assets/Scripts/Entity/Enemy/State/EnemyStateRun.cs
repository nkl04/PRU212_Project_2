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
        else if (enemy.CanAttackPlayer)
        {
            stateMachine.ChangeState(new EnemyStateAttack(enemy, stateMachine));
        }
    }

    public override void Enter()
    {
        enemy.CanFollowPlayer = true;
    }

    public override void Execute()
    {
        CheckChangeState();
        enemy.EnemyMovement.Move(enemy.EntityInfo._baseSpeed);
    }

    public override void Exit()
    {
    }
}
