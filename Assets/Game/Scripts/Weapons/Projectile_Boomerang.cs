using UnityEngine;

public class Projectile_Boomerang : Projectile
{
    [SerializeField] public Transform Visual;
    public float Range { get; set; }
    private Vector2 startPos;
    private bool isReturning = false;
    private float acceleration = 0.1f;
    private Vector3 direction;
    [SerializeField] private float rotateSpeed;
    private float lifeTimeCounter;

    private void OnEnable()
    {
        startPos = transform.position;

        isReturning = false;

        direction = transform.up;
        lifeTimeCounter = lifeTime;
    }


    protected override void Update()
    {
        if (!isReturning)
        {
            speed = speed - acceleration * Time.deltaTime;
        }
        else
        {
            speed = speed + acceleration * Time.deltaTime;
        }
        lifeTimeCounter -= Time.deltaTime;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        Visual.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (!isReturning && Vector2.Distance(startPos, transform.position) >= Range)
        {
            isReturning = true;
            direction = -direction;
        }

        if (lifeTimeCounter <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth_Base enemyHealth = collision.GetComponent<EnemyHealth_Base>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
