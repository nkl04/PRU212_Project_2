using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Skill Config", menuName = "Scriptable Objects/Config Skill", order = 0)]
public class ConfigSkill : ScriptableObject
{
    public string skillName;
    public List<SkillLevel> SkillLevelList;
    public GameObject skillController;
}

[Serializable]
public class SkillLevel
{
    public int level;
    public string description;
    public Sprite icon;
}