using Unity.VisualScripting;
using UnityEngine;

public class Shuriken : ShootWeapon
{
    protected override void Attack()
    {
        if (Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        Enemy_Base nearestEnemy = Player.PlayerAttack.GetTheNearestEnemy(Player.transform);

        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - Player.transform.position).normalized;

            GameObject shurikenBulletObj = ObjectPooler.Instance.GetObjectFromPool(_weaponInfo.bulletPrefab.name);

            shurikenBulletObj.transform.position = Player.transform.position;

            shurikenBulletObj.transform.up = direction;

            Projectile_Shuriken projectile_Shuriken = shurikenBulletObj.GetComponent<Projectile_Shuriken>();

            if (_weaponInfo is ConfigEquipmentWeapon config)
            {
                projectile_Shuriken.SetSpeed(config._speed);
                projectile_Shuriken.SetDamage(config._damage * ATKMultiplier);
                projectile_Shuriken.Direction = direction;
            }


            shurikenBulletObj.SetActive(true);
        }
    }

}
