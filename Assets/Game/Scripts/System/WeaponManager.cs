using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public Weapon Weapon => weaponPrefab.GetComponent<Weapon>();
    public PlayerController PlayerController { get; set; }
    [SerializeField] protected GameObject weaponPrefab;
    public abstract void ExecuteLevel(int level);
}
