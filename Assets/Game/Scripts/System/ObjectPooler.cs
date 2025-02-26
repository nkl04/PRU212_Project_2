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
                GameObject obj = Instantiate(pool.prefab, pool.parent ? pool.parent : this.transform);
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

        foreach (Pool pool in poolList)
        {
            if (pool.tag == tag && pool.expandable)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parent ? pool.parent : this.transform);
                obj.SetActive(false);
                pool.size++;
                poolDictionary[tag].Add(obj);
                return obj;
            }
        }
        return null;
    }

    /// <summary>
    /// Get amount of objects from pool with specific tag and amount regardless of state
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public GameObject[] GetAnyObjectsFromPool(string tag, int amount)
    {
        List<GameObject> objects = new List<GameObject>();
        int count = 0;

        // Lấy các object chưa active từ pool
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (obj)
            {
                objects.Add(obj);
                count++;

                if (count >= amount)
                    return objects.ToArray();
            }
        }

        foreach (Pool pool in poolList)
        {
            if (pool.tag == tag)
            {
                if (!pool.expandable)
                    return objects.ToArray();

                while (objects.Count < amount)
                {
                    GameObject newObj = Instantiate(pool.prefab, pool.parent ? pool.parent : this.transform);
                    newObj.SetActive(false);
                    poolDictionary[tag].Add(newObj);
                    objects.Add(newObj);
                }
                break;
            }
        }

        return objects.ToArray();
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

