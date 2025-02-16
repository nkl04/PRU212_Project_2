using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Image attackWeaponRate;
    private Weapon _weapon;
    private float timer = 0;

    /// <summary>
    /// Attack automatically
    /// </summary>
    public void AutomaticAttack()
    {
        timer += Time.deltaTime;
        attackWeaponRate.fillAmount = timer / _weapon._weaponInfo._attackSpeed;
        if (timer >= _weapon._weaponInfo._attackSpeed)
        {
            timer = 0;
            _weapon.Attack();
        }
    }
    public void SetWeapon(Weapon weapon, PlayerController player)
    {
        _weapon = weapon;
        _weapon.Player = player;
        _attackRange = weapon._weaponInfo._range;
    }
}
