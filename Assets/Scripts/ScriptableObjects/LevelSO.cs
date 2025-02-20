using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level SO")]
public class LevelSO : ScriptableObject
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
