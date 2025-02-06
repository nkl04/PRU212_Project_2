using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine<PlayerState> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<PlayerState>();
        stateMachine.ChangeState(new PlayerStateIdle(this, stateMachine));
    }
}
