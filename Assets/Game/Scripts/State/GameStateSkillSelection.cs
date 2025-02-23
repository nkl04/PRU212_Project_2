using UnityEngine;

public class GameStateSkillSelection : GameState
{
    public GameStateSkillSelection(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (gameManager.IsPaused)
        {
            stateMachine.ChangeState(gameManager.GameStatePause);
        }
        else if (!gameManager.IsPaused && !gameManager.IsPlayerLevelUp)
        {
            stateMachine.ChangeState(gameManager.GameStatePlaying);
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
