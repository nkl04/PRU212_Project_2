using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed;
    protected float damage;
    protected SpriteRenderer spriteRenderer;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    protected void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}