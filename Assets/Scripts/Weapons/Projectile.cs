using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed;
    protected float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    protected void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}