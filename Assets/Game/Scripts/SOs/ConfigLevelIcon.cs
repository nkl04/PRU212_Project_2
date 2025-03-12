using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config Level Icon", menuName = "Scriptable Objects/Config Level Icon")]
public class ConfigLevelIcon : ScriptableObject
{
    public List<Sprite> levelIcons;
}
