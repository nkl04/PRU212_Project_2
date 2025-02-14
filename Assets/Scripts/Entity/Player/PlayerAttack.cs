using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    private Weapon _weapon;
    private float _attackRange;
    private float timer = 0;

    /// <summary>
    /// Attack automatically
    /// </summary>
    public void AutomaticAttack()
    {
        timer += Time.deltaTime;
        if (timer >= _weapon._weaponInfo._attackSpeed)
        {
            timer = 0;
            _weapon.Attack();
        }
    }

    /// <summary>
    /// Get the nearest enemy from the player
    /// </summary>
    /// <returns></returns>

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    public void SetWeapon(Weapon weapon, Player player)
    {
        _weapon = weapon;
        _weapon.Player = player;
        _attackRange = weapon._weaponInfo._range;
    }
}
