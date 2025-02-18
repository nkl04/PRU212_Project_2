using UnityEngine;

public class EnemyHealth : MonoBehaviour, IAttackable
{
    private Enemy enemy;
    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // change state to die
            enemy.StateMachine.ChangeState(enemy.EnemyStateDie);
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }
    public void Die()
    {
        EventHandlers.CallOnEnemyDeadEvent(enemy);
        gameObject.SetActive(false);
    }
}
