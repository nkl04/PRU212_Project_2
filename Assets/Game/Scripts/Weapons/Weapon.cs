using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public PlayerController Player { get; set; }
    public ConfigWeapon _weaponInfo { get => weaponInfo; }
    [SerializeField] protected ConfigWeapon weaponInfo;
    private SpriteRenderer spriteRenderer;
    public abstract void Attack();
    private void OnValidate()
    {
        if (weaponInfo != null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = weaponInfo._sprite;
        }
    }
}


