using Unity.VisualScripting;
using UnityEngine;

public class Shuriken : Weapon
{
    protected override void Start()
    {
        Projectile_Shuriken projectile_Shuriken = _weaponInfo.bulletPrefab.GetComponent<Projectile_Shuriken>();
        if (projectile_Shuriken != null)
        {
            // projectile_Shuriken.SetSprite(_weaponInfo._sprite);
        }
    }
    public override void Attack()
    {
        if (Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        Enemy_Base nearestEnemy = Player.PlayerAttack.GetTheNeareastEnemy(Player.transform);

        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - Player.transform.position).normalized;

            GameObject shurikenBulletObj = ObjectPooler.Instance.GetObjectFromPool(_weaponInfo.bulletPrefab.name);

            shurikenBulletObj.transform.position = Player.transform.position;

            shurikenBulletObj.transform.up = direction;

            Projectile_Shuriken projectile_Shuriken = shurikenBulletObj.GetComponent<Projectile_Shuriken>();

            projectile_Shuriken.SetSpeed(_weaponInfo._speed);
            projectile_Shuriken.SetDamage(_weaponInfo._damage + Player.PlayerInfo._baseDamage);

            projectile_Shuriken.Direction = direction;

            shurikenBulletObj.SetActive(true);
        }
    }


}
