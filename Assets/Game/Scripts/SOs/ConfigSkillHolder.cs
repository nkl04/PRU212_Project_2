using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConfigSkillHolder", menuName = "Scriptable Objects/Config Skill Holder")]
public class ConfigSkillHolder : ScriptableObject
{
    public List<ConfigSkill> skillConfigs;

    private void OnValidate()
    {
        RefreshSkillConfigs();
    }

    public void RefreshSkillConfigs()
    {
        skillConfigs.Clear();

        string[] guids = AssetDatabase.FindAssets("t:ConfigSkill", new[] { "Assets/Game/Config/Skills" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ConfigSkill skillConfig = AssetDatabase.LoadAssetAtPath<ConfigSkill>(path);
            if (skillConfig != null)
            {
                skillConfigs.Add(skillConfig);
            }
        }
    }
}