using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    public Collider2D Collider2D => bodyCollider2d;
    public EnemyStateIdle EnemyStateIdle { get; private set; }
    public EnemyStateRun EnemyStateRun { get; private set; }
    public EnemyStateAttack EnemyStateAttack { get; private set; }
    public EnemyStateDie EnemyStateDie { get; private set; }
    public EnemyHealth_Base EnemyHealth { get; private set; }
    public EnemyMovement_Base EnemyMovement { get; private set; }
    public EnemyAttack_Base EnemyAttack { get; private set; }
    public EnemyRewardDrop EnemyRewardDrop { get; private set; }
    public ConfigEnemy EnemyInfo => enemyInfo;
    public PlayerController Target { get; private set; }
    public StateMachine<EnemyState> StateMachine => stateMachine;
    public Transform Visual => visual;
    public Animator Animator { get; private set; }
    public bool CanFollowPlayer { get; set; } = true;
    public bool CanAttackPlayer { get; set; } = false;

    [Header("Enemy Info")]
    [SerializeField] protected ConfigEnemy enemyInfo;
    [SerializeField] protected Transform visual;
    protected Collider2D bodyCollider2d;

    protected StateMachine<EnemyState> stateMachine;
    protected EnemyManager enemyManager;

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
            EnemyHealth.SetMaxHealth(enemyInfo._baseMaxHealth);
        }

        if (enemyManager != null)
        {
            enemyManager.RegisterEnemyGameObject(gameObject);
        }
    }

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<EnemyState>();
        EnemyHealth = GetComponent<EnemyHealth_Base>();
        EnemyMovement = GetComponent<EnemyMovement_Base>();
        EnemyAttack = GetComponent<EnemyAttack_Base>();
        EnemyRewardDrop = GetComponent<EnemyRewardDrop>();
        Animator = GetComponent<Animator>();

        enemyManager = FindFirstObjectByType<EnemyManager>();
        Target = FindFirstObjectByType<PlayerController>();
        bodyCollider2d = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        EnemyStateIdle = new EnemyStateIdle(this, stateMachine);
        EnemyStateRun = new EnemyStateRun(this, stateMachine);
        EnemyStateAttack = new EnemyStateAttack(this, stateMachine);
        EnemyStateDie = new EnemyStateDie(this, stateMachine);

        EnemyHealth.SetMaxHealth(enemyInfo._baseMaxHealth);
        enemyManager.RegisterEnemyGameObject(gameObject);
        EnemyRewardDrop.SetReward(enemyInfo.RewardList);
        stateMachine.ChangeState(EnemyStateIdle);
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void OnDisable()
    {
        EnemyHealth.SetMaxHealth(enemyInfo._baseMaxHealth);
        enemyManager.UnregisterEnemyGameObject(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    { }

    protected virtual void OnTriggerExit2D(Collider2D other)
    { }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
    }
}
