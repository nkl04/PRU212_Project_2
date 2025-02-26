using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public void SetCurrentSkillIcon(ConfigSkill configSkill, Sprite icon)
    {
        try
        {
            Image nextIcon;
            if (configSkill is ConfigSkillPassive)
            {
                nextIcon = passiveSkillIcon.FirstOrDefault(x => x != null && x.gameObject != null && !x.gameObject.activeSelf);
            }
            else
            {
                nextIcon = activeSkillIcon.FirstOrDefault(x => x != null && x.gameObject != null && !x.gameObject.activeSelf);
            }

            if (nextIcon != null)
            {
                nextIcon.sprite = icon;
                nextIcon.gameObject.SetActive(true);
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
