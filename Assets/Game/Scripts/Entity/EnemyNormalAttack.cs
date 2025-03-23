using UnityEngine;

[RequireComponent(typeof(EnemyNormal))]
public class EnemyNormalAttack : EnemyAttack_Base
{
    public override void Attack(IAttackable attackable, float damage)
    {
        attackable?.TakeDamage(damage);
    }
}
