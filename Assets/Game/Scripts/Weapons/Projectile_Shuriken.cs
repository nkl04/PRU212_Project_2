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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.transform.TryGetComponent<IAttackable>(out IAttackable attackable) && !collision.CompareTag("Player"))
        {
            DisableSelf();
        }
    }
}
