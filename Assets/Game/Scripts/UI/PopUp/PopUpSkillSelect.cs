using UnityEngine;

public class PopUpSkillSelect : MonoBehaviour
{
    [SerializeField] private SkillSelectPanel[] skillSelectPanels;


    public void SetSkills((ConfigSkill, int)[] configLevelSkillArray)
    {
        for (int i = 0; i < skillSelectPanels.Length; i++)
        {
            skillSelectPanels[i].SetSkill(configLevelSkillArray[i].Item1, configLevelSkillArray[i].Item2);
        }
    }
}
