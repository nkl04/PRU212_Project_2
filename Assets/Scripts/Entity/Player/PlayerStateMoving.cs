using UnityEngine;

public class PlayerStateMoving : PlayerState
{
    private Vector2 _directionVector;
    private float _speed;
    public PlayerStateMoving(PlayerController player, StateMachine<PlayerState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.Animator.Play(Utilities.AnimationClips.Player.Run);
    }

    public override void Execute()
    {
        CheckChangeState();

        _directionVector = player.DirectionVector;
        _speed = player.PlayerInfo._baseSpeed;

        player.transform.parent.position += new Vector3(_directionVector.x, _directionVector.y, 0) * _speed * Time.deltaTime;
    }

    public override void Exit()
    {
    }

    public override void CheckChangeState()
    {
        if (!player.IsMoving)
        {
            stateMachine.ChangeState(new PlayerStateIdle(player, stateMachine));
        }
    }
}
