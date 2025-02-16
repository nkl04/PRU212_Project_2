using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField] private Image healthBar;
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
        healthBar.fillAmount = currentHealth / maxHealth;
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
