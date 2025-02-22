using UnityEngine;

public class ConfigWeapon : ScriptableObject
{
    public string _name;
    public float _coolDown;
    public float _speed;
    public float _range;
    public Sprite _sprite;
    public GameObject bulletPrefab;
    public Weapon weapon;

    private void OnValidate()
    {
        if (bulletPrefab != null && bulletPrefab.GetComponent<Projectile>() == null)
        {
            Debug.LogError($"GameObject {bulletPrefab.name} must contain Projectile component!", bulletPrefab);
            bulletPrefab = null;
        }
    }
}
