using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    [SerializeField] private ConfigSkillHolder _skillHolder;
    private Dictionary<ConfigSkill, int> skillLevels;
    protected override void Awake()
    {
        base.Awake();
        skillLevels = new Dictionary<ConfigSkill, int>();

        foreach (var skillConfig in _skillHolder.skillConfigs)
        {
            skillLevels[skillConfig] = 0;
        }

        EventHandlers.OnLevelUpEvent += EventHandlers_OnLevelUpEvent;
    }

    private void EventHandlers_OnLevelUpEvent(int level)
    {
        var upgradableSkills = skillLevels
            .Where(pair => pair.Key.SkillLevelList.Count > pair.Value + 1)
            .OrderBy(_ => UnityEngine.Random.value)
            .Take(3)
            .Select(pair => (pair.Key, pair.Value + 1))
            .ToArray();

        UIManager.Instance.UpdatePopUpSkill(upgradableSkills);
    }
    public void LevelUpSkill(ConfigSkill skillConfig)
    {
        if (skillConfig == null || !skillLevels.ContainsKey(skillConfig))
        {
            Debug.LogError("Skill configuration is invalid or not found!");
            return;
        }

        int currentLevel = skillLevels[skillConfig];

        if (currentLevel < skillConfig.SkillLevelList.Count - 1)
        {
            skillLevels[skillConfig]++;
            skillConfig.weaponManager.GetComponent<IWeaponManager>().ExecuteLevel(skillLevels[skillConfig]);
            Debug.Log($"{skillConfig.skillName} leveled up to level {skillLevels[skillConfig] + 1}");
        }
        else
        {
            Debug.Log($"{skillConfig.skillName} is already at max level!");
        }
    }

    public void ResetSkill(ConfigSkill skillConfig)
    {
        if (skillConfig == null || !skillLevels.ContainsKey(skillConfig))
        {
            Debug.LogError("Skill configuration is invalid or not found!");
            return;
        }

        skillLevels[skillConfig] = 0;
        Debug.Log($"{skillConfig.skillName} reset to level 1");
    }

    public int GetSkillLevel(ConfigSkill skillConfig)
    {
        if (skillConfig == null || !skillLevels.ContainsKey(skillConfig))
        {
            Debug.LogError("Skill configuration is invalid or not found!");
            return -1;
        }

        return skillLevels[skillConfig];
    }
}