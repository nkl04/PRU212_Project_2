using UnityEngine;

public class GameStatePlaying : GameState
{
    public GameStatePlaying(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (gameManager.IsPaused)
        {
            stateMachine.ChangeState(gameManager.GameStatePause);
        }
        else if (gameManager.IsPlayerLevelUp && !gameManager.IsPaused)
        {
            stateMachine.ChangeState(gameManager.GameStateSkillSelection);
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