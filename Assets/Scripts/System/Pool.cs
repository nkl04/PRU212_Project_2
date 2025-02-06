using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
    public bool expandable;
    public bool hasMax;
    public int max;
}