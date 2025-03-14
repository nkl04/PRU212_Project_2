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

    private new void Awake()
    {
        base.Awake();

        finishedLevelDictionary = new Dictionary<ConfigLevel, bool>();
        foreach (var item in configLevels.levels)
        {
            finishedLevelDictionary.Add(item, false);
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
            case GameState.Splash:
                Time.timeScale = 1;
                break;
        }

        EventHandlers.CallOnGameStateUpdateEvent(gameState);
    }

    private void HandleMainMenuState()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    private void HandleGamePlayState()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
    }

    public void FinishLevel(int index)
    {
        finishedLevelDictionary[configLevels.levels[index]] = true;
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    End,
    Splash
}


