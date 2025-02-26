using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    public abstract void CheckChangeState();
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
