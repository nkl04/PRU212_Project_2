using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkillSelect : MonoBehaviour
{
    [SerializeField] private SkillSelectPanel[] skillSelectPanels;
    [SerializeField] private List<Transform> activeSkillIcon;
    [SerializeField] private List<Transform> passiveSkillIcon;
    public void SetSkills((ConfigSkill, int)[] configLevelSkillArray)
    {
        for (int i = 0; i < skillSelectPanels.Length; i++)
        {
            skillSelectPanels[i].SetSkill(configLevelSkillArray[i].Item1, configLevelSkillArray[i].Item2);
            skillSelectPanels[i].SetNewTextActive(configLevelSkillArray[i].Item2 <= 1);
        }
    }

    public void SetCurrentSkillIcons(Dictionary<ConfigSkill, int> currentSkills)
    {
        var passiveSkills = currentSkills.Where(skill => skill.Key is ConfigSkillPassive).ToList();
        var activeSkills = currentSkills.Where(skill => !(skill.Key is ConfigSkillPassive)).ToList();

        UpdateSkillIcons(activeSkillIcon, activeSkills);
        UpdateSkillIcons(passiveSkillIcon, passiveSkills);
    }

    private void UpdateSkillIcons(List<Transform> skillIcons, List<KeyValuePair<ConfigSkill, int>> skills)
    {
        for (int i = 0; i < skills.Count && i < skillIcons.Count; i++)
        {
            skillIcons[i].GetComponent<Image>().sprite = skills[i].Key.SkillLevelList[skills[i].Value].icon;
            skillIcons[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
