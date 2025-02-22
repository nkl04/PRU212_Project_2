using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Active Skill Config", menuName = "Scriptable Objects/Config Skill Active")]
public class ConfigSkillActive : ConfigSkill
{
    public List<SkillLevel> SkillLevelList;
}

[Serializable]
public class SkillLevel
{
    public int level;
    public string description;
    public Sprite icon;
    public UnityEvent execute;
}