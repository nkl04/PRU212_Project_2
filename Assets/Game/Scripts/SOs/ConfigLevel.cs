using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config Level", menuName = "Scriptable Objects/Config Level")]
public class ConfigLevel : ScriptableObject
{
    public Sprite background;
    public List<Wave> waveList;

    public Wave GetWave(int waveIndex)
    {
        if (waveIndex < waveList.Count)
        {
            return waveList[waveIndex];
        }
        else
        {
            Debug.LogWarning("Wave index out of range");
            return null;
        }
    }

    public int GetWaveIndex(Wave wave)
    {
        return waveList.IndexOf(wave);
    }

}
