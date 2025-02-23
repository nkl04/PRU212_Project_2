using UnityEngine;

public class KunaiManager : MonoBehaviour, IWeaponManager
{
    [SerializeField] private GameObject kunaiWeaponPrefab;
    private Shuriken kunaiWeapon;

    public void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                GameObject kunaiWeaponGameObj = ObjectPooler.Instance.GetObjectFromPool(kunaiWeaponPrefab.name);
                kunaiWeaponGameObj.SetActive(true);
                kunaiWeapon = kunaiWeaponGameObj.GetComponent<Shuriken>();
                kunaiWeapon.oneTimeBulletAmount = 1;
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
