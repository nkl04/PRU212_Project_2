using UnityEngine;

public class Boomerang : LaunchWeapon
{
    private float sizeMultiple = 1;
    public void AddProjectileSize(float multiple)
    {
        sizeMultiple = multiple;
    }
    protected override void Attack()
    {
        if (Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        IAttackable nearestAttackable = Player.PlayerAttack.GetTheNearestAttackableObject(Player.transform);

        if (nearestAttackable != null)
        {
            Vector2 direction = (nearestAttackable.Transform.position - Player.transform.position).normalized;

            GameObject boomerangGameObj = ObjectPooler.Instance.GetObjectFromPool(_weaponInfo.bulletPrefab.name);

            boomerangGameObj.transform.position = Player.transform.position;

            boomerangGameObj.transform.up = direction;

            Projectile_Boomerang projectile_Boomerang = boomerangGameObj.GetComponent<Projectile_Boomerang>();
            if (_weaponInfo is ConfigSkillWeapon copnfig)
            {
                projectile_Boomerang.SetSpeed(copnfig._speed);
                projectile_Boomerang.SetDamage(ATKMultiplier * Player.PlayerStats.Damage);
                projectile_Boomerang.SetLifeTime(copnfig.lifeTime);
                projectile_Boomerang.Range = (copnfig._range);
                projectile_Boomerang.SetSizeMultiple(sizeMultiple);
            }
            boomerangGameObj.SetActive(true);
        }
    }


}
