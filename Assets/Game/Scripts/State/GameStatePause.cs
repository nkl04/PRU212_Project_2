using UnityEngine;

public class GameStatePause : GameState
{
    public GameStatePause(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (!gameManager.IsPaused && !gameManager.IsPlayerLevelUp)
        {
            stateMachine.ChangeState(gameManager.GameStatePlaying);
        }
        else if (!gameManager.IsPaused && gameManager.IsPlayerLevelUp)
        {
            stateMachine.ChangeState(gameManager.GameStateSkillSelection);
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

