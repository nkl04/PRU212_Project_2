using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public PlayerController Player { get; set; }
    public ConfigWeapon _weaponInfo { get => weaponInfo; }
    public int oneTimeBulletAmount { get; set; } = 1;
    public float ATKMultiplier { get; set; } = 1;
    [SerializeField] protected ConfigWeapon weaponInfo;
    private SpriteRenderer spriteRenderer;

    protected bool _canAttack = true;
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
    public virtual void StartAttack()
    {
        if (_canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    protected abstract IEnumerator AttackRoutine();
}

public abstract class ShootWeapon : Weapon
{
    protected override IEnumerator AttackRoutine()
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
public abstract class LaunchWeapon : Weapon
{
    private List<GameObject> activeProjectiles = new List<GameObject>();

    protected override IEnumerator AttackRoutine()
    {
        _currentBulletAmount = oneTimeBulletAmount;
        _canAttack = false;

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

        while (activeProjectiles.Count > 0)
        {
            yield return null;
            activeProjectiles.RemoveAll(proj => proj == null || !proj.activeSelf);
        }

        _canAttack = true;
    }
}
public abstract class OrbitingWeapon : Weapon
{
    protected override IEnumerator AttackRoutine()
    {
        _canAttack = false;

        float elapsedTime = 0f;
        float lifeTime = ((ConfigSkillWeapon)_weaponInfo).lifeTime;

        Attack();

        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Disappear();

        yield return new WaitForSeconds(weaponInfo._coolDown);
        _canAttack = true;
    }


    protected abstract void Disappear();
}


