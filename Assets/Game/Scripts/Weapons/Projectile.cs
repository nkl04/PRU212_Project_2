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
    private Vector3 initScale;
    private void Awake()
    {
        initScale = transform.localScale;
    }
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

    public void SetSizeMultiple(float multiple)
    {
        transform.localScale = initScale * multiple;
    }

    protected void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<IAttackable>(out IAttackable attackable) && !collision.CompareTag("Player"))
        {
            if (attackable != null)
            {
                attackable.TakeDamage(damage);
            }

            if (attackable is EnemyHealth_Base)
            {
                GameObject damagePopup = ObjectPooler.Instance.GetObjectFromPool("DamagePopup");
                damagePopup.transform.position = collision.transform.position;
                damagePopup.GetComponent<DamagePopup>().SetText(damage.ToString());
                damagePopup.SetActive(true);
            }
        }
    }
}