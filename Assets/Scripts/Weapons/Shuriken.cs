using UnityEngine;

public class Shuriken : Weapon
{
    public override void Attack()
    {
        if (Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        Enemy nearestEnemy = Player.GetTheNeareastEnemy(Player.transform);

        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - Player.transform.position).normalized;

            GameObject shurikenBulletObj = ObjectPooler.Instance.GetObjectFromPool(_weaponInfo.bulletPrefab.name);

            shurikenBulletObj.transform.position = Player.transform.position;

            shurikenBulletObj.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            Projectile_Shuriken projectile_Shuriken = shurikenBulletObj.GetComponent<Projectile_Shuriken>();

            projectile_Shuriken.Direction = direction;

            projectile_Shuriken.SetDamage(_weaponInfo._damage + Player.PlayerInfo._baseDamage);

            projectile_Shuriken.SetSpeed(_weaponInfo._speed);

            shurikenBulletObj.SetActive(true);
        }
    }
}
