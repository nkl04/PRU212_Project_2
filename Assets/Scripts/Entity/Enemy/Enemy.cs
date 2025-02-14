using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    [Header("Enemy Info")]
    [SerializeField] private EntityInfo entityInfo;
    public GameObject player;
    public float hp = 1;
    private Vector3 _originScale;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.SetMaxHealth(entityInfo._baseMaxHealth);
    }

    private void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        _originScale = transform.localScale * 1f;

    }

    private void Update()
    {
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, entityInfo._baseSpeed * Time.deltaTime);
            // var l1 = 0.1f * (hp - 1);
            // var scaleTo = _originScale + Vector3.one * l1;
            // scaleTo.z = _originScale.z;
            // transform.localScale = scaleTo;
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
            IAttackable attackable = other.GetComponent<IAttackable>();
            attackable?.TakeDamage(entityInfo._baseDamage);
        }
        // if (other.transform.CompareTag("Bullet"))
        // {
        //     // Calc HP here
        //     hp -= 1;
        //     if (hp <= 0)
        //     {
        //         Destroy(gameObject);
        //     }

        //     // other.GetComponent<Bullet>().canDestroy = true;
        // }
        // else
        // {
        //     // if (other.transform.CompareTag("Enemy"))
        //     // {
        //     //     var ec = other.GetComponent<Enemy>();
        //     //     if (ec.hp > hp || (Math.Abs(ec.hp - hp) < 0.001f && ec.speed > entityInfo._speed))
        //     //     {
        //     //         ec.hp += hp;
        //     //         ec.speed = (speed + ec.speed) / 2f;
        //     //         Destroy(gameObject);
        //     //     }
        //     // }
        // }
    }
}
