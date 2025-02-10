using UnityEngine;
using ControlFreak2;
using System;

public class Player : MonoBehaviour
{

    public EntityInfo PlayerInfo { get => playerInfo; }
    public Vector2 DirectionVector { get; set; }
    public bool IsMoving { get; set; }
    private StateMachine<PlayerState> stateMachine;
    private GameInput gameInput;
    private PlayerAttack playerAttack;

    [Header("Data")]
    [SerializeField] private EntityInfo playerInfo;

    [Header("Gear")]
    [SerializeField] private Weapon weapon;

    private void Awake()
    {
        stateMachine = new StateMachine<PlayerState>();

        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.SetWeapon(weapon);
    }
    private void Start()
    {
        gameInput = GameInput.Instance;

        gameInput.OnMovePerformed += OnMove;
        gameInput.OnMoveCanceled += OnMove;

        stateMachine.ChangeState(new PlayerStateIdle(this, stateMachine));


    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnMove(Vector2 directionVector)
    {
        DirectionVector = directionVector;
        IsMoving = directionVector.magnitude > 0;
    }
}
