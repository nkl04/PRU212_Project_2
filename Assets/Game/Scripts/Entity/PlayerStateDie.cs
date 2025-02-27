using UnityEngine;

public class PlayerStateDie : PlayerState
{
    public PlayerStateDie(PlayerController player, StateMachine<PlayerState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
    }

    public override void Enter()
    {
        player.PlayerAttack.CanAttack = false;
        player.Animator.Play(Utilities.AnimationClips.Player.Die);
        //player.PlayerHealth.Die();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
