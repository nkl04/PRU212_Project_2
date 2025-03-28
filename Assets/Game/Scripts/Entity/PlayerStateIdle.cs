using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(PlayerController player, StateMachine<PlayerState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.Animator.Play(Utilities.AnimationClips.Player.Idle);
    }

    public override void Execute()
    {
        CheckChangeState();
    }

    public override void Exit()
    {
    }

    public override void CheckChangeState()
    {
        if (player.PlayerMovement.IsMoving)
        {
            stateMachine.ChangeState(player.PlayerStateRun);
        }
    }
}
