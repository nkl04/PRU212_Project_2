using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField] private Image healthBar;
    private float maxHealth;
    private float currentHealth;

    private PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();

        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage * (1 - player.PlayerStats.DamageReduction);
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            player.StateMachine.ChangeState(player.PlayerStateDie);
        }
    }

    public void RestoreHealth(float restoreHealthMul)
    {
        currentHealth += maxHealth * restoreHealthMul;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        EventHandlers.CallOnPlayerDeadEvent();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
}
