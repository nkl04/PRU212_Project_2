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
    [SerializeField] private float _detectRange;
    private float timer = 0;

    /// <summary>
    /// Attack automatically
    /// </summary>
    public void AutomaticAttack()
    {
        timer += Time.deltaTime;
        attackWeaponRate.fillAmount = timer / _weapon._weaponInfo._coolDown;
        if (timer >= _weapon._weaponInfo._coolDown)
        {
            timer = 0;
            _weapon.StartAttack();
        }
    }

    public void SetUpWeapon(PlayerController player)
    {
        GameObject weaponObj = Instantiate(player.PlayerInfo.defaultWeaponInfo.weapon.gameObject, handPosition);
        Weapon weapon = weaponObj.GetComponent<Weapon>();

        _weapon = weapon;
        _weapon.Player = player;
    }

    /// <summary>
    /// Get the nearest enemy
    /// </summary>
    public Enemy_Base GetTheNeareastEnemy(Transform transform)
    {
        Transform playerTransform = transform;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, _detectRange);
        Enemy_Base nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            Enemy_Base enemy = collider.GetComponent<Enemy_Base>();
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
