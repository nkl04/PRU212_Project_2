using System;
using UnityEngine;

public class PopUpSkillSelect : MonoBehaviour
{
    [SerializeField] private SkillSelectPanel[] skillSelectPanels;

    public void SetSkills((ConfigSkill, int)[] configLevelSkillArray)
    {
        try
        {
            for (int i = 0; i < skillSelectPanels.Length; i++)
            {
                skillSelectPanels[i].SetSkill(configLevelSkillArray[i].Item1, configLevelSkillArray[i].Item2);
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
