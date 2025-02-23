using UnityEngine;

public class GameStateGameplay : GameState
{
    public GameStateGameplay(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
        if (gameManager.IsPaused)
        {
            SetSubState(gameManager.GameStatePause);
        }
        else if (gameManager.IsPlayerLevelUp && !gameManager.IsPaused)
        {
            SetSubState(gameManager.GameStateSkillSelection);
        }
    }

    public override void CheckChangeState()
    {
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
