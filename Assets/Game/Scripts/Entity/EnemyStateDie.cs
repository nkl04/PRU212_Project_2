using UnityEngine;

public class EnemyStateDie : EnemyState
{
    public EnemyStateDie(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
    }

    public override void Enter()
    {
        //drop reward 
        enemy.EnemyRewardDrop.DropReward();
        //call on enemy dead event
        EventHandlers.CallOnEnemyDeadEvent(enemy);
        // die animation
        enemy.Animator.Play(Utilities.AnimationClips.Enemy.Die);

        ////die
        //enemy.EnemyHealth.Die();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
