using UnityEngine;

public abstract class EnemyAttack_Base : MonoBehaviour
{
    public abstract void Attack(IAttackable attackable, float damage);
}
