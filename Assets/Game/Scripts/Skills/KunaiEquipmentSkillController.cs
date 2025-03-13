using UnityEngine;


public class KunaiEquipmentSkillController : WeaponSkillController
{
    private Shuriken weapon;
    public override void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                GameObject kunaiWeaponGameObj = Instantiate(weaponPrefab, PlayerController.HandPosition);
                kunaiWeaponGameObj.SetActive(true);
                weapon = kunaiWeaponGameObj.GetComponent<Shuriken>();
                weapon.oneTimeBulletAmount = 1;
                weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                PlayerController.PlayerAttack.AddWeapon(weapon);
                break;
            case 2:
                if (weapon != null)
                {
                    weapon.oneTimeBulletAmount = 2;
                    // add more damage
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 3:
                if (weapon != null)
                {
                    weapon.oneTimeBulletAmount = 3;
                    // add more damage
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 4:
                if (weapon != null)
                {
                    weapon.oneTimeBulletAmount = 4;
                    // add more damage
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
            case 5:
                if (weapon != null)
                {
                    weapon.oneTimeBulletAmount = 5;
                    // add more damage
                    weapon.ATKMultiplier = weapon._weaponInfo.configSkillActive.ATKMuliplier[level - 1];
                }
                break;
        }
    }
}
