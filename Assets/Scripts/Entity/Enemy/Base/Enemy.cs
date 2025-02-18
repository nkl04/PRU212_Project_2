using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyStateIdle EnemyStateIdle { get; private set; }
    public EnemyStateRun EnemyStateRun { get; private set; }
    public EnemyStateAttack EnemyStateAttack { get; private set; }
    public EnemyStateDie EnemyStateDie { get; private set; }
    public EnemyHealth EnemyHealth { get; private set; }
    public EnemyMovement EnemyMovement { get; private set; }
    public EnemyAttack EnemyAttack { get; private set; }
    public EnemyRewardDrop EnemyRewardDrop { get; private set; }
    public EntityInfo EntityInfo => entityInfo;
    public PlayerController Target { get; private set; }
    public StateMachine<EnemyState> StateMachine => stateMachine;
    public Animator Animator { get; private set; }
    public bool CanFollowPlayer { get; set; } = true;
    public bool CanAttackPlayer { get; set; } = false;

    [Header("Enemy Info")]
    [SerializeField] protected EntityInfo entityInfo;

    protected StateMachine<EnemyState> stateMachine;

    private void OnEnable()
    {
        if (stateMachine != null)
        {
            if (EnemyStateIdle != null && EnemyStateRun != null && EnemyStateAttack != null && EnemyStateDie != null)
            {
                stateMachine.ChangeState(EnemyStateIdle);
            }
        }

        if (EnemyHealth != null)
        {
            EnemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
        }
    }

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<EnemyState>();
        EnemyHealth = GetComponent<EnemyHealth>();
        EnemyMovement = GetComponent<EnemyMovement>();
        EnemyAttack = GetComponent<EnemyAttack>();
        EnemyRewardDrop = GetComponent<EnemyRewardDrop>();
        Animator = GetComponent<Animator>();

        EnemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
        Target = FindFirstObjectByType<PlayerController>();
    }

    protected virtual void Start()
    {
        EnemyStateIdle = new EnemyStateIdle(this, stateMachine);
        EnemyStateRun = new EnemyStateRun(this, stateMachine);
        EnemyStateAttack = new EnemyStateAttack(this, stateMachine);
        EnemyStateDie = new EnemyStateDie(this, stateMachine);

        EnemyRewardDrop.SetReward(entityInfo.RewardList);
        stateMachine.ChangeState(EnemyStateIdle);
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void OnDisable()
    {
        EnemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    { }

    protected virtual void OnTriggerExit2D(Collider2D other)
    { }
}
