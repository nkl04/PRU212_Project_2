using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    public abstract void Spawn();
}
