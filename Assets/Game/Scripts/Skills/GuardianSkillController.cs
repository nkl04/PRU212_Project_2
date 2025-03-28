using UnityEngine;

public class GuardianSkillController : WeaponSkillController
{
    private Guardian weapon;
    public override void ExecuteLevel(int level)
    {
        if (PlayerController == null)
        {
            Debug.Log("Player is null");
            return;
        }

        switch (level)
        {
            case 1:
                GameObject gameObject = Instantiate(weaponPrefab, PlayerController.SkillWeaponTransform);
                weapon = gameObject.GetComponent<Guardian>();
                weapon.oneTimeBulletAmount = 2;
                GameObject[] projectTileObj = ObjectPooler.Instance.GetAnyObjectsFromPool(weapon._weaponInfo.bulletPrefab.name, weapon.oneTimeBulletAmount);
                foreach (GameObject obj in projectTileObj)
                {
                    obj.transform.SetParent(weapon.ProjectileSystem);
                }
                weapon.SetUpSaw();
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                PlayerController.PlayerAttack.AddWeapon(weapon);
                break;
            case 2:
                Instantiate(weapon._weaponInfo.bulletPrefab, weapon.ProjectileSystem);
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                weapon.RotateSpeed *= 1.1f;
                weapon.SetUpSaw();
                break;
            case 3:
                Instantiate(weapon._weaponInfo.bulletPrefab, weapon.ProjectileSystem);
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                weapon.RotateSpeed *= 1.2f;
                weapon.SetUpSaw();
                break;
            case 4:
                Instantiate(weapon._weaponInfo.bulletPrefab, weapon.ProjectileSystem);
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                weapon.RotateSpeed *= 1.3f;
                weapon.SetUpSaw();
                break;
            case 5:
                Instantiate(weapon._weaponInfo.bulletPrefab, weapon.ProjectileSystem);
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                weapon.RotateSpeed *= 1.4f;
                weapon.SetUpSaw();
                break;
        }
    }

}
