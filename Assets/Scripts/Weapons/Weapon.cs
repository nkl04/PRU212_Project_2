using UnityEngine;

public class Weapon : MonoBehaviour
{
    public EquipmentInfo _weaponInfo { get => weaponInfo; }
    [SerializeField] private EquipmentInfo weaponInfo;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (weaponInfo != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = weaponInfo._sprite;
        }
    }
}
