using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool CanFollowPlayer { get; set; } = true;

    [Header("Enemy Info")]
    [SerializeField] private EntityInfo entityInfo;
    private GameObject target;
    private EnemyHealth enemyHealth;
    private StateMachine<EnemyState> stateMachine;
    private bool facingRight = true; // Lưu trạng thái hướng hiện tại của enemy

    private void Awake()
    {
        stateMachine = new StateMachine<EnemyState>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new EnemyStateIdle(this, stateMachine));
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public void MoveTowardsTarget()
    {
        if (!target) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, entityInfo._baseSpeed * Time.deltaTime);

        Flip(direction.x);
    }

    private void Flip(float directionX)
    {
        if ((directionX > 0 && !facingRight) || (directionX < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnDisable()
    {
        enemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanFollowPlayer = false;
            IAttackable attackable = other.GetComponent<IAttackable>();
            attackable?.TakeDamage(entityInfo._baseDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanFollowPlayer = true;
        }
    }
}
