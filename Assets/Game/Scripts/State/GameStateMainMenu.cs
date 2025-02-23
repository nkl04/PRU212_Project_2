using UnityEngine;

public class GameStateMainMenu : GameState
{
    public GameStateMainMenu(GameManager gameManager, StateMachine<GameState> stateMachine) : base(gameManager, stateMachine)
    {
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
