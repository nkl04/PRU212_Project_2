using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public PlayerController Player { get; set; }
    public EquipmentInfo _weaponInfo { get => weaponInfo; }
    [SerializeField] protected EquipmentInfo weaponInfo;
    private SpriteRenderer spriteRenderer;

    protected abstract void Start();
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


