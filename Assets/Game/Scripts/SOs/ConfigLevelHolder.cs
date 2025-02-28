using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConfigLevelHolder", menuName = "Scriptable Objects/Config Level Holder")]
public class ConfigLevelHolder : ScriptableObject
{
    public List<ConfigLevel> levels;

    private void OnValidate()
    {
        LoadConfigLevel();
    }
    public void LoadConfigLevel()
    {
        levels.Clear();
        var guids = AssetDatabase.FindAssets("t:ConfigLevel", new[] { "Assets/Game/Config/Level" });
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var level = AssetDatabase.LoadAssetAtPath<ConfigLevel>(path);
            if (level != null)
            {
                levels.Add(level);
            }
        }
    }
}
