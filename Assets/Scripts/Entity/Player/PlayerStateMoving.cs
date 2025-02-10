using UnityEngine;

public class PlayerStateMoving : PlayerState
{
    private Vector2 _directionVector;
    private float _speed;
    public PlayerStateMoving(Player player, StateMachine<PlayerState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        _directionVector = player.DirectionVector;
        _speed = player.PlayerInfo._speed;

        player.transform.parent.position += new Vector3(_directionVector.x, _directionVector.y, 0) * _speed * Time.deltaTime;
    }

    public override void Exit()
    {
        CheckChangeState();
    }

    public override void CheckChangeState()
    {
        if (!player.IsMoving)
        {
            stateMachine.ChangeState(new PlayerStateIdle(player, stateMachine));
        }
    }
}
