using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(Player player, StateMachine<PlayerState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    public override void CheckChangeState()
    {

    }
}
