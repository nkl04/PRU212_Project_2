using UnityEngine;

public class GameStatePause : GameState
{
    public GameStatePause(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (gameManager.IsPaused)
        {
            stateMachine.ChangeState(gameManager.GameStatePause);
        }
    }

    public override void Enter()
    {
        Time.timeScale = 0;

    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}

