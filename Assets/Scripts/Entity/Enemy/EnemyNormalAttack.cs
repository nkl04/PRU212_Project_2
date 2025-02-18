using UnityEngine;

[RequireComponent(typeof(EnemyNormal))]
public class EnemyNormalAttack : EnemyAttack
{
    public override void Attack(IAttackable attackable, float damage)
    {
        attackable?.TakeDamage(damage);
    }
}
