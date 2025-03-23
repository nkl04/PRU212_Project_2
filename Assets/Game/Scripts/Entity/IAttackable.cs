using UnityEngine;

public interface IAttackable
{
    public Transform Transform { get; }
    public bool IsDead { get; }
    public void TakeDamage(float damage);
    public void Die();
}
