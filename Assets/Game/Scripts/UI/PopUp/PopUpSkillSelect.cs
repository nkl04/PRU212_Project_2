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
    [SerializeField] private List<Image> activeSkillIcon;
    [SerializeField] private List<Image> passiveSkillIcon;
    public void SetSkills((ConfigSkill, int)[] configLevelSkillArray)
    {
        try
        {
            for (int i = 0; i < skillSelectPanels.Length; i++)
            {
                skillSelectPanels[i].SetSkill(configLevelSkillArray[i].Item1, configLevelSkillArray[i].Item2);
                skillSelectPanels[i].SetNewTextActive(configLevelSkillArray[i].Item2 <= 1);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return;
        }
    }

    public void SetCurrentSkillIcons(Dictionary<ConfigSkill, int> currentSkills)
    {
        try
        {
            ClearSkillIcons(activeSkillIcon);
            ClearSkillIcons(passiveSkillIcon);

            var passiveSkills = currentSkills.Where(skill => skill.Key is ConfigSkillPassive).ToList();
            var activeSkills = currentSkills.Where(skill => !(skill.Key is ConfigSkillPassive)).ToList();

            UpdateSkillIcons(activeSkillIcon, activeSkills);
            UpdateSkillIcons(passiveSkillIcon, passiveSkills);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void UpdateSkillIcons(List<Image> skillIcons, List<KeyValuePair<ConfigSkill, int>> skills)
    {
        if (skillIcons == null || skills == null) return;

        for (int i = 0; i < skills.Count && i < skillIcons.Count; i++)
        {
            skillIcons[i].sprite = skills[i].Key.SkillLevelList[skills[i].Value].icon;
            skillIcons[i].gameObject.SetActive(true);
        }
    }


    public void ClearSkillIcons(List<Image> imageList)
    {
        try
        {
            if (imageList == null) return;
            foreach (var child in imageList)
            {
                child.sprite = null;
                child.gameObject.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
