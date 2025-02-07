using UnityEngine;
using ControlFreak2;

public class Player : MonoBehaviour
{

    public EntityInfo PlayerInfo { get => playerInfo; }
    private StateMachine<PlayerState> stateMachine;

    [Header("Data")]
    [SerializeField] private EntityInfo playerInfo;

    private void Start()
    {
        stateMachine = new StateMachine<PlayerState>();
        stateMachine.ChangeState(new PlayerStateIdle(this, stateMachine));
    }

    private void Update()
    {

    }
}
