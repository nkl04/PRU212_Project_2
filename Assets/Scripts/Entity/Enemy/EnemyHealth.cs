using UnityEngine;

public class EnemyHealth : MonoBehaviour, IAttackable
{
    private float maxHealth;
    private float currentHealth;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
