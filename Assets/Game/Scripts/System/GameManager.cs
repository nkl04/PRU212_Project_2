using ControlFreak2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
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
        foreach (var item in configLevels.levels)
        {
            finishedLevelDictionary.Add(item, false);
            levelBestTimeDictionary.Add(item, (0, 0));
        }
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


