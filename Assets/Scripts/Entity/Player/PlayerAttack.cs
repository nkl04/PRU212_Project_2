using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    [Header("HandPosition")]
    [SerializeField] private Transform handPosition;

    [Header("UI")]
    [SerializeField] private Image attackWeaponRate;

    private Weapon _weapon;
    private float _attackRange;
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

    public void SetUpWeapon(PlayerController player)
    {
        GameObject weaponObj = Instantiate(player.PlayerInfo.weaponInfo.weapon.gameObject, handPosition);
        Weapon weapon = weaponObj.GetComponent<Weapon>();

        _weapon = weapon;
        _weapon.Player = player;
        _attackRange = weapon._weaponInfo._range;
    }

    /// <summary>
    /// Get the nearest enemy
    /// </summary>
    public Enemy GetTheNeareastEnemy(Transform transform)
    {
        Transform playerTransform = transform;
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
}
