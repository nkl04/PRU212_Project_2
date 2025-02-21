using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "Scriptable Objects/Skill/Active Skill SO")]
public class ActiveSkillSO : SkillSO
{
    public List<SkillLevel> SkillLevelList;
}

[Serializable]
public class SkillLevel
{
    public int level;
    public string description;
    public Sprite icon;
    public ActiveSkill activeSkillActivation;
}