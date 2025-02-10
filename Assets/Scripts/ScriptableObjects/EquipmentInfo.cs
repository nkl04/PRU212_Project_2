using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Info", menuName = "Scriptable Objects/Equipment Info")]
public class EquipmentInfo : ScriptableObject
{
    public string _name;
    public int _damage;
    public float _attackSpeed;
    public float _range;
    public Sprite _sprite;
    public GameObject _model;
}
