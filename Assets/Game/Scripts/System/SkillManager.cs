using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    [SerializeField] private ConfigSkillHolder _skillHolder;
    [SerializeField] private PlayerController playerController;
    private Dictionary<ConfigSkill, int> skillLevels;
    private Dictionary<ConfigSkill, WeaponManager> weaponManagers;
    protected override void Awake()
    {
        base.Awake();
        skillLevels = new Dictionary<ConfigSkill, int>();
        weaponManagers = new Dictionary<ConfigSkill, WeaponManager>();

        foreach (var skillConfig in _skillHolder.skillConfigs)
        {
            skillLevels[skillConfig] = 0;
            GameObject weaponManagerObj = Instantiate(skillConfig.weaponManager, transform);
            WeaponManager weaponManager = weaponManagerObj.GetComponent<WeaponManager>();
            weaponManager.PlayerController = playerController;

            weaponManagers[skillConfig] = weaponManager;
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
        #region Debug Skill
        Debug.Log($"<color=yellow>Random select Skills:</color>");
        foreach (var skill in upgradableSkills)
        {
            Debug.Log($"<color=yellow>Skill: {skill.Key.skillName}, Next Level: {skill.Item2}</color>");
        }
        #endregion

        EventHandlers.CallOnRandomSkillsEvent(upgradableSkills);
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

            if (weaponManagers.TryGetValue(skillConfig, out WeaponManager weaponManager))
            {
                weaponManager.ExecuteLevel(skillLevels[skillConfig]);
                Debug.Log($"<color=green>Upgrade skill: {skillConfig.skillName} leveled up to level {skillLevels[skillConfig]}</color>");
            }
            else
            {
                Debug.LogError($"WeaponManager not found for {skillConfig.skillName}");
            }
        }
        else
        {
            Debug.Log($"{skillConfig.skillName} is already at max level!");
        }
    }

    public void AddConfigSkill(ConfigSkill skillConfig)
    {
        if (skillConfig == null || skillLevels.ContainsKey(skillConfig))
        {
            Debug.LogError("Skill configuration is invalid or already exists!");
            return;
        }

        skillLevels[skillConfig] = 0;
        GameObject weaponManagerObj = Instantiate(skillConfig.weaponManager, transform);
        WeaponManager weaponManager = weaponManagerObj.GetComponent<WeaponManager>();
        weaponManager.PlayerController = playerController;
        weaponManagers[skillConfig] = weaponManager;

        Debug.Log($"{skillConfig.skillName} added to skill manager");
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