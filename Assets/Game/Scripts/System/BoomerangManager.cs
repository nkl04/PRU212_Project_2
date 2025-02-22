using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    [SerializeField] private GameObject boomerangPrefab;

    private List<GameObject> boomerangList;
    public void ExecuteLevel1()
    {
        GameObject boomerangGameObj = ObjectPooler.Instance.GetObjectFromPool(boomerangPrefab.name);
        boomerangGameObj.transform.position = transform.position;
        boomerangGameObj.SetActive(true);

        boomerangList.Add(boomerangGameObj);
    }

    public void ExecuteLevel2()
    {
        GameObject boomerangGameObj = ObjectPooler.Instance.GetObjectFromPool(boomerangPrefab.name);
        boomerangGameObj.transform.position = transform.position;
        boomerangGameObj.SetActive(true);

        boomerangList.Add(boomerangGameObj);
    }

    public void ExecuteLevel3()
    {
        foreach (GameObject boomerang in boomerangList)
        {
            //doublet damage

        }
    }

    public void ExecuteLevel4()
    {
        foreach (GameObject boomerang in boomerangList)
        {
            // add size

            // more damage
        }
    }

    public void ExecuteLevel5()
    {
        foreach (GameObject boomerang in boomerangList)
        {
            // Reverse polarity.

        }
    }

}
