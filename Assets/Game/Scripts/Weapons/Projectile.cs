using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public abstract class Projectile : MonoBehaviour
{
    protected float speed;
    protected float damage;
    protected float lifeTime;
    protected SpriteRenderer spriteRenderer;
    protected abstract void Update();
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject damagePopup = ObjectPooler.Instance.GetObjectFromPool("DamagePopup");
            damagePopup.transform.position = collision.transform.position;
            damagePopup.GetComponent<DamagePopup>().SetText(damage.ToString());
            damagePopup.SetActive(true);
        }
    }
}