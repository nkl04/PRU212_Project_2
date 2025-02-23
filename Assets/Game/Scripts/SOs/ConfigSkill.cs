using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Active Skill Config", menuName = "Scriptable Objects/Config Skill Active")]
public class ConfigSkill : ScriptableObject
{
    public string skillName;
    public List<SkillLevel> SkillLevelList;
    public GameObject weaponManager;
}

[Serializable]
public class SkillLevel
{
    public int level;
    public string description;
    public Sprite icon;
}