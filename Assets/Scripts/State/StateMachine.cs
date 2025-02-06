using UnityEngine;

public class StateMachine<T> : MonoBehaviour where T : State
{
    private T currentState;

    public void ChangeState(T newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
            currentState.CheckChangeState();
        }
    }
}
