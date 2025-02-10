using UnityEngine;

[CreateAssetMenu(fileName = "New Info", menuName = "Scriptable Objects/Entity Info", order = 1)]
public class EntityInfo : ScriptableObject
{
    public string _name;
    public float _maxHealth;
    public float _speed;
}
