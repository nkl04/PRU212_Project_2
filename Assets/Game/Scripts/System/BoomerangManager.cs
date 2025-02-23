using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangManager : MonoBehaviour, IWeaponManager
{
    [SerializeField] private GameObject boomerangWeaponPrefab;
    private Boomerang boomerangWeapon;

    public void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                GameObject boomerangWeaponGameObj = ObjectPooler.Instance.GetObjectFromPool(boomerangWeaponPrefab.name);
                boomerangWeaponGameObj.transform.position = Vector3.zero;
                boomerangWeaponGameObj.SetActive(true);
                boomerangWeapon = boomerangWeaponGameObj.GetComponent<Boomerang>();
                boomerangWeapon.oneTimeBulletAmount = 1;
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
