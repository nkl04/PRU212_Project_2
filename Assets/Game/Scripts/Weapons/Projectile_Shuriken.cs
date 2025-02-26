using UnityEngine;

public class Projectile_Shuriken : Projectile
{
    public Vector2 Direction { get; set; }
    protected override void Update()
    {
        if (Direction != null)
        {
            transform.position += (Vector3)Direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth_Base enemyHealth = collision.GetComponent<EnemyHealth_Base>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            DisableSelf();
        }
    }
}
