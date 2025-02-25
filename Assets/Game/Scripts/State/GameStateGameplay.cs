using UnityEngine;

public class GameStateGameplay : GameState
{
    public GameStateGameplay(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (gameManager.IsPaused)
        {
            stateMachine.ChangeState(gameManager.GameStatePause);
        }
        else if (gameManager.IsEndLevel)
        {
            stateMachine.ChangeState(gameManager.GameStateLevelEnd);
        }
    }

    public override void Enter()
    {
        Time.timeScale = 1;
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
