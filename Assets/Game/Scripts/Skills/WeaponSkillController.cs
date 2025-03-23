using UnityEngine;

public abstract class SkillController : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }
    public abstract void ExecuteLevel(int level);
}
public abstract class WeaponSkillController : SkillController
{
    public Weapon Weapon => weaponPrefab.GetComponent<Weapon>();
    [SerializeField] protected GameObject weaponPrefab;
}

public abstract class SupplySkillController : SkillController
{
}
