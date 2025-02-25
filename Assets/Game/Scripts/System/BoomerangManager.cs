using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangManager : WeaponManager
{
    private Boomerang boomerangWeapon;
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
                boomerangWeapon = boomerangWeaponGameObj.GetComponent<Boomerang>();
                boomerangWeapon.oneTimeBulletAmount = 1;
                Debug.Log(PlayerController == null);
                PlayerController.PlayerAttack.AddWeapon(boomerangWeapon);
                break;
            case 2:
                if (boomerangWeapon != null)
                {
                    boomerangWeapon.oneTimeBulletAmount = 2;
                }
                break;
            case 3:
                if (boomerangWeapon != null)
                {
                    boomerangWeapon.Multiplier = 2;
                }
                break;
            case 4:
                if (boomerangWeapon != null)
                {
                    //add size

                }
                break;
            case 5:
                if (boomerangWeapon != null)
                {
                    //add size

                }
                break;
        }
    }
}
