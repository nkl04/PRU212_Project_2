using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill Config", menuName = "Scriptable Objects/Config Skill Active", order = 1)]
public class ConfigSkillActive : ConfigSkill
{
    public List<float> ATKMuliplier;
}
