using ControlFreak2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public Dictionary<ConfigLevel, bool> FinishedLevelDictionary => finishedLevelDictionary;
    public ConfigLevelHolder ConfigLevelHolder => configLevels;
    public ConfigLevelIcon ConfigLevelIcons => configLevelIcons;
    public ConfigLevel SelectedLevel { get; set; }
    public bool IsPaused { get; set; } = false;
    public bool IsEndLevel { get; set; } = false;
    public bool IsPlayerLevelUp { get; set; } = false;

    [SerializeField] private ConfigLevelHolder configLevels;
    [SerializeField] private ConfigLevelIcon configLevelIcons;

    private Dictionary<ConfigLevel, bool> finishedLevelDictionary;
    private Dictionary<ConfigLevel, (int, int)> levelBestTimeDictionary;

    private new void Awake()
    {
        base.Awake();

        finishedLevelDictionary = new Dictionary<ConfigLevel, bool>();
        levelBestTimeDictionary = new Dictionary<ConfigLevel, (int, int)>();

        LoadData();
    }

    public void UpdateGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                HandleMainMenuState();
                break;
            case GameState.Gameplay:
                Time.timeScale = 1;
                HandleGamePlayState();
                break;
            case GameState.End:
                Time.timeScale = 0;
                break;
            case GameState.Win:
                Time.timeScale = 0;
                HandleWinState();
                break;
            case GameState.Splash:
                Time.timeScale = 1;
                break;
        }

        EventHandlers.CallOnGameStateUpdateEvent(gameState);
    }

    private void HandleWinState()
    {
        FinishLevel(SelectedLevel.levelIndex);
    }

    private void HandleMainMenuState()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    private void HandleGamePlayState()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
    }
    public void FinishLevel(int levelIndex)
    {
        finishedLevelDictionary[configLevels.levels[levelIndex]] = true;
        if (levelIndex + 1 < configLevels.levels.Count)
            SelectedLevel = configLevels.levels[levelIndex + 1];
        SaveData();
    }
    public bool IsFinishLevel(int levelIndex)
    {
        return finishedLevelDictionary[configLevels.levels[levelIndex]];
    }
    public (int, int) GetBestTimeInLevel(ConfigLevel configLevel)
    {
        return levelBestTimeDictionary[configLevel];
    }
    public void UpdateBestTimeInLevel(ConfigLevel configLevel, (int minute, int second) bestTime)
    {
        levelBestTimeDictionary[configLevel] = bestTime;
        SaveData();
    }
    public bool IsBestTimeInLevel(ConfigLevel configLevel, (int minute, int second) time)
    {
        if (!levelBestTimeDictionary.TryGetValue(configLevel, out var currentBestTime) ||
            time.minute < currentBestTime.Item1 ||
            (time.minute == currentBestTime.Item1 && time.second < currentBestTime.Item2))
        {
            return false;
        }
        return true;
    }

    private void LoadData()
    {
        #region Load Selected Level
        if (PlayerPrefs.HasKey(Utilities.PlayerPrefs.SELECTED_LEVEL))
        {
            int selectedLevelIndex = PlayerPrefs.GetInt(Utilities.PlayerPrefs.SELECTED_LEVEL);
            SelectedLevel = configLevels.levels[selectedLevelIndex];
        }
        else
        {
            SelectedLevel = configLevels.levels[0];
        }
        #endregion

        #region Load Finished Levels
        if (PlayerPrefs.HasKey(Utilities.PlayerPrefs.FINISHED_LEVELS))
        {
            string json = PlayerPrefs.GetString(Utilities.PlayerPrefs.FINISHED_LEVELS);
            SerializableFinishedLevels loadedData = JsonUtility.FromJson<SerializableFinishedLevels>(json);

            foreach (var item in configLevels.levels)
            {
                finishedLevelDictionary[item] = loadedData.completedLevels.Contains(item.levelIndex);
            }
        }
        else
        {
            foreach (var item in configLevels.levels)
            {
                finishedLevelDictionary[item] = false;
            }
        }
        #endregion

        #region Load Best Times
        if (PlayerPrefs.HasKey(Utilities.PlayerPrefs.BEST_TIME))
        {
            string json = PlayerPrefs.GetString(Utilities.PlayerPrefs.BEST_TIME);
            List<SerializableBestTime> loadedTimes = JsonUtility.FromJson<SerializableBestTimeList>(json).times;

            foreach (var time in loadedTimes)
            {
                ConfigLevel level = configLevels.levels.Find(l => l.levelIndex == time.levelIndex);
                if (level != null)
                {
                    levelBestTimeDictionary[level] = (time.minute, time.second);
                }
            }
        }
        else
        {
            foreach (var item in configLevels.levels)
            {
                levelBestTimeDictionary[item] = (0, 0);
            }
        }
        #endregion
    }

    public void SaveData()
    {

        Debug.Log("Save Data");
        #region Save SelectedLevel
        if (SelectedLevel != null)
        {
            PlayerPrefs.SetInt(Utilities.PlayerPrefs.SELECTED_LEVEL, SelectedLevel.levelIndex);
        }
        #endregion

        #region Save Finished Levels
        SerializableFinishedLevels finishedData = new SerializableFinishedLevels();
        foreach (var pair in finishedLevelDictionary)
        {
            if (pair.Value) finishedData.completedLevels.Add(pair.Key.levelIndex);
        }
        PlayerPrefs.SetString(Utilities.PlayerPrefs.FINISHED_LEVELS, JsonUtility.ToJson(finishedData));

        #endregion

        #region Save Best Times
        SerializableBestTimeList bestTimes = new SerializableBestTimeList();
        foreach (var pair in levelBestTimeDictionary)
        {
            bestTimes.times.Add(new SerializableBestTime(pair.Key.levelIndex, pair.Value.Item1, pair.Value.Item2));
        }
        PlayerPrefs.SetString(Utilities.PlayerPrefs.BEST_TIME, JsonUtility.ToJson(bestTimes));
        #endregion

        PlayerPrefs.Save();
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    End,
    Win,
    Splash
}

[System.Serializable]
public class SerializableFinishedLevels
{
    public List<int> completedLevels = new List<int>();

    public Dictionary<int, bool> ToDictionary()
    {
        Dictionary<int, bool> result = new Dictionary<int, bool>();
        foreach (int level in completedLevels)
        {
            result[level] = true;
        }
        return result;
    }
}


[System.Serializable]
public class SerializableBestTime
{
    public int levelIndex;
    public int minute;
    public int second;

    public SerializableBestTime(int levelIndex, int minute, int second)
    {
        this.levelIndex = levelIndex;
        this.minute = minute;
        this.second = second;
    }
}

[System.Serializable]
public class SerializableBestTimeList
{
    public List<SerializableBestTime> times = new List<SerializableBestTime>();
}

