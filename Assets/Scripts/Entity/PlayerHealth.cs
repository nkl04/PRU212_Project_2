using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField] private Image healthBar;
    [SerializeField] private ParticleSystem hitEffect;
    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            //healthBar.fillAmount = currentHealth / maxHealth;
        }
        hitEffect.Play();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
}
