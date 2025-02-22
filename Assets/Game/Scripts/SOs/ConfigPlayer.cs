using UnityEngine;

[CreateAssetMenu(fileName = "New Config Player", menuName = "Scriptable Objects/Config Player")]
public class ConfigPlayer : ConfigEntity
{
    [Header("Gear")]
    public ConfigWeapon weaponInfo;
}
