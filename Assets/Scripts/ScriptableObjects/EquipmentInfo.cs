using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Info", menuName = "Scriptable Objects/Equipment Info")]
public class EquipmentInfo : ScriptableObject
{
    public string _name;
    public int _damage;
    public float _attackSpeed;
    public float _range;
    public Sprite _sprite;
    public GameObject bulletPrefab;
    public Weapon weapon;
    public FireMoveType fireMoveType;

    private void OnValidate()
    {
        if (bulletPrefab != null && bulletPrefab.GetComponent<Bullet>() == null)
        {
            Debug.LogError($"GameObject {bulletPrefab.name} must contain Bullet component!", bulletPrefab);
            bulletPrefab = null;
        }
    }
}

public enum FireMoveType
{
    StraightShot,
    MultiShot
}