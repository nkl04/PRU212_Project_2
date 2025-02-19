using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level SO")]
public class LevelSO : ScriptableObject
{
    public Sprite background;
    public List<Wave> waveList;
    public List<float> timeBetweenWaveList;

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

    private void OnValidate()
    {
        if (waveList != null && waveList.Count > 0)
        {
            if (timeBetweenWaveList == null)
            {
                timeBetweenWaveList = new List<float>();
            }

            if (timeBetweenWaveList.Count != waveList.Count - 1)
            {
                int difference = waveList.Count - 1 - timeBetweenWaveList.Count;

                if (difference > 0)
                {
                    for (int i = 0; i < difference; i++)
                    {
                        timeBetweenWaveList.Add(0f);
                    }
                }
                else
                {
                    for (int i = 0; i < -difference; i++)
                    {
                        timeBetweenWaveList.RemoveAt(timeBetweenWaveList.Count - 1);
                    }
                }
            }
        }
    }
}
