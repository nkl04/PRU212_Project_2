using UnityEngine;

[CreateAssetMenu(fileName = "New Player Info", menuName = "Scriptable Objects/Player Info")]
public class PlayerInfo : EntityInfo
{
    [Header("Gear")]
    public EquipmentInfo weaponInfo;
}
