using UnityEngine;

public class EnemyStateRun : EnemyState
{
    public EnemyStateRun(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (!enemy.CanFollowPlayer || Vector2.Distance(enemy.transform.position, enemy.Target.transform.position) >= 30f)
        {
            stateMachine.ChangeState(enemy.EnemyStateIdle);
        }
        else if (enemy.CanAttackPlayer)
        {
            stateMachine.ChangeState(enemy.EnemyStateAttack);
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
