using System.Collections.Generic;
using UnityEngine;

public class ConfigEntity : ScriptableObject
{
    [Header("Base Info")]
    public string _name;
    public float _baseMaxHealth;
    public float _baseDamage;
    public float _baseSpeed;
}
