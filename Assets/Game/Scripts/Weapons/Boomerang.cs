using UnityEngine;

public class Boomerang : Weapon
{
    protected override void Attack()
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

            GameObject boomerangGameObj = ObjectPooler.Instance.GetObjectFromPool(_weaponInfo.bulletPrefab.name);

            boomerangGameObj.transform.position = Player.transform.position;

            boomerangGameObj.transform.up = direction;

            Projectile_Boomerang projectile_Boomerang = boomerangGameObj.GetComponent<Projectile_Boomerang>();
            if (_weaponInfo is ConfigSkillWeapon copnfig)
            {
                projectile_Boomerang.SetSpeed(copnfig._speed);
                projectile_Boomerang.SetDamage(copnfig.ATK_Multiplier * Multiplier * Player.PlayerInfo._baseDamage);
                projectile_Boomerang.Range = (copnfig._range);
            }
            boomerangGameObj.SetActive(true);
        }
    }
}
