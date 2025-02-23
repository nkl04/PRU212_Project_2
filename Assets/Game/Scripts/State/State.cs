using UnityEngine;

public abstract class State
{
    protected State superState;
    protected State subState;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    public abstract void CheckChangeState();
    public void UpdateStates()
    {
        Execute();
        if (subState != null)
        {
            subState.Execute();
        }
    }
    public void ExitStates()
    {
        Exit();
        if (subState != null)
        {
            subState.Exit();
        }
    }
    protected void SetSuperState(State state)
    {
        superState = state;
    }
    protected void SetSubState(State state)
    {
        subState = state;
        state.SetSuperState(this);
    }
}

public abstract class PlayerState : State
{
    protected PlayerController player;
    protected StateMachine<PlayerState> stateMachine;

    public PlayerState(PlayerController player, StateMachine<PlayerState> stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
}

public abstract class EnemyState : State
{
    protected Enemy_Base enemy;
    protected StateMachine<EnemyState> stateMachine;

    public EnemyState(Enemy_Base enemy, StateMachine<EnemyState> stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
}

public abstract class GameState : State
{
    protected GameManager gameManager;
    protected StateMachine<GameState> stateMachine;

    public GameState(GameManager gameManager, StateMachine<GameState> stateMachine)
    {
        this.gameManager = gameManager;
        this.stateMachine = stateMachine;
    }
}