using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPause : MonoBehaviour
{
    [SerializeField] private Transform[] activeSkillSlotItems;
    [SerializeField] private Transform[] passiveSkillSlotItems;

    public void SetCurrentSkillIcons(Dictionary<ConfigSkill, int> currentSkills)
    {
        try
        {
            var passiveSkills = currentSkills.Where(skill => skill.Key is ConfigSkillPassive).ToList();
            var activeSkills = currentSkills.Where(skill => !(skill.Key is ConfigSkillPassive)).ToList();

            UpdateSkillIcons(activeSkillSlotItems, activeSkills);
            UpdateSkillIcons(passiveSkillSlotItems, passiveSkills);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void UpdateSkillIcons(Transform[] skillIconTransforms, List<KeyValuePair<ConfigSkill, int>> skills)
    {

        for (int i = 0; i < skills.Count && i < skillIconTransforms.Length; i++)
        {
            Transform child = skillIconTransforms[i];
            Transform childChild = child.GetChild(0);
            childChild.GetComponent<Image>().sprite = skills[i].Key.SkillLevelList[skills[i].Value].icon;

            StarLevel starLevel = child.GetComponentInChildren<StarLevel>();
            starLevel.SetStarLevel(skills[i].Value);

            child.gameObject.SetActive(true);
        }
    }
}
