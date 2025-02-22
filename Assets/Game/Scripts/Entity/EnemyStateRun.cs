using UnityEngine;

public class EnemyStateRun : EnemyState
{
    public EnemyStateRun(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
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
        enemy.Animator.Play(Utilities.AnimationClips.Enemy.Run);
    }

    public override void Execute()
    {
        CheckChangeState();
        enemy.EnemyMovement.Move(enemy.EnemyInfo._baseSpeed);
    }

    public override void Exit()
    {
    }
}
