using DG.Tweening;
using UnityEngine;

public class EnemyHealth_Base : MonoBehaviour, IAttackable
{
    private Enemy_Base enemy;
    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        enemy = GetComponent<Enemy_Base>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // change state to die
            enemy.StateMachine.ChangeState(enemy.EnemyStateDie);
            return;
        }
        BeAttackedAnimation();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    private void BeAttackedAnimation()
    {
        //change color to white and back to normal
        SpriteRenderer visualSprite = enemy.Visual.GetComponent<SpriteRenderer>();
        visualSprite.DOColor(Color.red, 0.1f).OnComplete(() => visualSprite.DOColor(Color.white, 0.1f));
        transform.DOScaleX(0.7f, 0.1f).OnComplete(() => transform.DOScaleX(1f, 0.1f));
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
