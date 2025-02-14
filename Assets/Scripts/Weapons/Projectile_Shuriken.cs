using UnityEngine;

public class Projectile_Shuriken : Projectile
{
    public Vector2 Direction { get; set; }
    private void Update()
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
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            DisableSelf();
        }
    }
}
