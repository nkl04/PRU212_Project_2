using UnityEngine;

public class StateMachine<T> where T : State
{
    private T currentState;

    public void ChangeState(T newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Execute();
    }
}
