using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSkillController : WeaponSkillController
{
    private Boomerang weapon;
    public override void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                if (PlayerController == null)
                {
                    Debug.Log("Player is null");
                    return;
                }
                GameObject boomerangWeaponGameObj = Instantiate(weaponPrefab, PlayerController.SkillWeaponTransform);
                weapon = boomerangWeaponGameObj.GetComponent<Boomerang>();
                weapon.oneTimeBulletAmount = 1;
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                PlayerController.PlayerAttack.AddWeapon(weapon);
                break;
            case 2:
                if (weapon != null)
                {
                    weapon.oneTimeBulletAmount = 2;
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 3:
                if (weapon != null)
                {
                    //add size
                    weapon.AddProjectileSize(1.2f);
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 4:
                if (weapon != null)
                {
                    //add size
                    weapon.AddProjectileSize(1.3f);
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 5:
                if (weapon != null)
                {
                    //add size
                    weapon.AddProjectileSize(1.4f);
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
        }
    }
}
