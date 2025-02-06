using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField] private List<Pool> poolList;
    [SerializeField] private Dictionary<string, List<GameObject>> poolDictionary;
    private new void Awake()
    {
        base.Awake();
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in poolList)
        {
            List<GameObject> objectPoolQueue = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPoolQueue.Add(obj);
            }
            poolDictionary.Add(pool.tag, objectPoolQueue);
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                return poolDictionary[tag][i];
            }
        }

        foreach (Pool item in poolList)
        {
            if (item.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab, this.transform);
                obj.SetActive(false);
                item.size++;
                poolDictionary[tag].Add(obj);
                return obj;
            }
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (poolList != null)
        {
            foreach (Pool pool in poolList)
            {
                if (pool.prefab != null)
                {
                    pool.tag = pool.prefab.name;
                }
            }
        }
    }
#endif
}

