using UnityEngine;
using ControlFreak2;
using System;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public PlayerInfo PlayerInfo { get => playerInfo; }
    public Vector2 DirectionVector { get; set; }
    public bool IsMoving { get; set; }
    private StateMachine<PlayerState> stateMachine;
    private GameInput gameInput;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private bool isFacingRight = true;

    [Header("Data")]
    [SerializeField] private PlayerInfo playerInfo;

    [Header("References")]
    [SerializeField] private Transform handPosition;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        stateMachine = new StateMachine<PlayerState>();

        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();

        SetUpWeapon();

        playerHealth.SetMaxHealth(playerInfo._baseMaxHealth);
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
        playerAttack.AutomaticAttack();
        stateMachine.Update();
    }

    private void OnMove(Vector2 directionVector)
    {
        DirectionVector = directionVector;
        IsMoving = directionVector.magnitude > 0;

        if (isFacingRight && directionVector.x < 0)
        {
            Flip();
            isFacingRight = false;
        }
        else if (!isFacingRight && directionVector.x > 0)
        {
            Flip();
            isFacingRight = true;
        }
    }
    private void SetUpWeapon()
    {
        GameObject weaponObj = Instantiate(playerInfo.weaponInfo.weapon.gameObject, handPosition);
        Weapon weapon = weaponObj.GetComponent<Weapon>();
        playerAttack.SetWeapon(weapon, this);
    }
    public Enemy GetTheNeareastEnemy(Transform playerTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, playerInfo.weaponInfo._range);
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(playerTransform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
