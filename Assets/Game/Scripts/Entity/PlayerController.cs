using UnityEngine;
using ControlFreak2;
using System;

public class PlayerController : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    public PlayerStateIdle PlayerStateIdle { get; private set; }
    public PlayerStateMoving PlayerStateRun { get; private set; }
    public PlayerStateDie PlayerStateDie { get; private set; }
    public StateMachine<PlayerState> StateMachine => stateMachine;
    public Transform SkillWeaponTransform => skillWeaponTransform;
    public Transform HandPosition => handPosition;

    [SerializeField] private Transform skillWeaponTransform;
    [SerializeField] private Transform handPosition;

    private StateMachine<PlayerState> stateMachine;

    private GameInput gameInput;

    [Header("Data")]
    [SerializeField] private ConfigPlayer playerInfo;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        stateMachine = new StateMachine<PlayerState>();

        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerStats = GetComponent<PlayerStats>();

        PlayerStats.MaxHealth = playerInfo._baseMaxHealth;
        PlayerStats.Damage = playerInfo._baseDamage;
        PlayerStats.MoveSpeed = playerInfo._baseSpeed;

        PlayerHealth.SetMaxHealth(PlayerStats.MaxHealth);

    }
    private void Start()
    {
        gameInput = GameInput.Instance;

        PlayerStateIdle = new PlayerStateIdle(this, stateMachine);
        PlayerStateRun = new PlayerStateMoving(this, stateMachine);
        PlayerStateDie = new PlayerStateDie(this, stateMachine);

        gameInput.OnMovePerformed += PlayerMovement.OnMove;
        gameInput.OnMoveCanceled += PlayerMovement.OnMove;

        stateMachine.ChangeState(PlayerStateIdle);

        SkillManager.Instance.AddConfigSkill(playerInfo.defaultWeaponManager.Weapon._weaponInfo.configSkill);
        SkillManager.Instance.LevelUpSkill(playerInfo.defaultWeaponManager.Weapon._weaponInfo.configSkill);

        //PlayerStats.Damage += ((ConfigEquipmentWeapon)playerInfo.defaultWeaponManager.Weapon._weaponInfo)._damage;
    }

    private void Update()
    {
        PlayerAttack.AutomaticAttack();
        stateMachine.Update();
    }
}
