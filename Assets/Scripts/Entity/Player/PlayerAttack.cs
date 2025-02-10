using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    private Weapon _weapon;
    private float _attackRange;
    /// <summary>
    /// Attack the nearest enemy
    /// </summary>
    public void Attack()
    {
        // Get the nearest enemy
        Enemy enemy = GetTheNeareastEnemy(transform);

        // if (enemy != null)
        // {
        //     // Attack the enemy
        //     _weapon.Attack(enemy);
        // }

    }

    /// <summary>
    /// Get the nearest enemy from the player
    /// </summary>
    /// <returns></returns>
    public Enemy GetTheNeareastEnemy(Transform playerTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, _attackRange);
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(playerTransform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _attackRange = weapon._weaponInfo._range;
    }

}
