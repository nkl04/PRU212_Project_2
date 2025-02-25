using UnityEngine;


public class KunaiManager : WeaponManager
{
    private Shuriken kunaiWeapon;
    public override void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                GameObject kunaiWeaponGameObj = Instantiate(weaponPrefab, PlayerController.HandPosition);
                kunaiWeaponGameObj.SetActive(true);
                kunaiWeapon = kunaiWeaponGameObj.GetComponent<Shuriken>();
                kunaiWeapon.oneTimeBulletAmount = 1;
                PlayerController.PlayerAttack.AddWeapon(kunaiWeapon);
                break;
            case 2:
                if (kunaiWeapon != null)
                {
                    kunaiWeapon.oneTimeBulletAmount = 2;

                    // add more damage
                }
                break;
            case 3:
                if (kunaiWeapon != null)
                {
                    kunaiWeapon.oneTimeBulletAmount = 3;

                    // add more damage

                }
                break;
            case 4:
                if (kunaiWeapon != null)
                {
                    kunaiWeapon.oneTimeBulletAmount = 4;

                    // add more damage

                }
                break;
            case 5:
                if (kunaiWeapon != null)
                {
                    kunaiWeapon.oneTimeBulletAmount = 5;

                    // add more damage

                }
                break;
        }
    }
}
