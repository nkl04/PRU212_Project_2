using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public Player Player { get; set; }
    public EquipmentInfo _weaponInfo { get => weaponInfo; }
    [SerializeField] protected EquipmentInfo weaponInfo;
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


