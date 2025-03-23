using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    public bool CanAttack { get; set; } = true;
    private PlayerController playerController;

    [Header("HandPosition")]
    [SerializeField] private Transform handPosition;

    [Header("UI")]
    [SerializeField] private Image attackWeaponRate;

    private List<Weapon> _weapons = new List<Weapon>();
    private Dictionary<Weapon, float> _attackTimers = new Dictionary<Weapon, float>();

    [SerializeField] private float _detectRange;
    [SerializeField] private LayerMask _attackableLayer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (CanAttack)
        {
            AutomaticAttack();
        }
    }

    /// <summary>
    /// Tự động tấn công với tất cả vũ khí
    /// </summary>
    public void AutomaticAttack()
    {
        if (_weapons.Count == 0) return;

        for (int i = 0; i < _weapons.Count; i++)
        {
            var weapon = _weapons[i];

            if (!_attackTimers.ContainsKey(weapon))
                _attackTimers[weapon] = 0;

            _attackTimers[weapon] += Time.deltaTime;

            if (i == 0)
            {
                attackWeaponRate.fillAmount = _attackTimers[weapon] / weapon._weaponInfo._coolDown;
            }

            if (_attackTimers[weapon] >= weapon._weaponInfo._coolDown)
            {
                _attackTimers[weapon] = 0;
                weapon.StartAttack();
            }
        }
    }


    public void AddWeapon(Weapon newWeapon)
    {
        newWeapon.Player = playerController;
        _weapons.Add(newWeapon);
        _attackTimers[newWeapon] = 0;
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (_weapons.Contains(weapon))
        {
            _weapons.Remove(weapon);
            _attackTimers.Remove(weapon);
            Destroy(weapon.gameObject);
        }
    }

    public IAttackable GetTheNearestAttackableObject(Transform transform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, _attackableLayer);
        IAttackable nearestIAttackable = null;
        float minDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            IAttackable attackable = collider.GetComponent<IAttackable>();

            if (attackable != null && !attackable.IsDead)
            {
                float distance = Vector2.Distance(transform.position, attackable.Transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIAttackable = attackable;
                }
            }
        }
        return nearestIAttackable;
    }
}
