using System.Collections;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public PlayerController Player { get; set; }
    public ConfigWeapon _weaponInfo { get => weaponInfo; }
    public int oneTimeBulletAmount { get; set; } = 2;
    public int Multiplier { get; set; } = 1;
    [SerializeField] protected ConfigWeapon weaponInfo;
    private SpriteRenderer spriteRenderer;
    private bool _canAttack = true;
    protected int _currentBulletAmount;
    protected abstract void Attack();
    private void OnValidate()
    {
        if (weaponInfo != null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = weaponInfo._sprite;
        }
    }

    public void StartAttack()
    {
        if (_canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        _currentBulletAmount = oneTimeBulletAmount;
        while (_currentBulletAmount > 0)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            Attack();
            _currentBulletAmount--;
            yield return new WaitForSeconds(weaponInfo._attackRate);
        }
    }
}


