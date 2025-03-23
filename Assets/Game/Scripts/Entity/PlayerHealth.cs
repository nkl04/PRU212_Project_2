using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField] private Image healthBar;
    private float maxHealth;
    private float currentHealth;

    private PlayerController player;

    public bool IsDead => currentHealth <= 0;

    public Transform Transform => transform;

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
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            return;
        }
        currentHealth += maxHealth * restoreHealthMul;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.End);
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
}
