using ControlFreak2;
using System;
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

    private new void Awake()
    {
        base.Awake();
    }

    public void UpdateGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;
            case GameState.Gameplay:
                Time.timeScale = 1;
                HandleGamePlayState();
                break;
            case GameState.Pause:
                Time.timeScale = 0;
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

    private void HandleGamePlayState()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
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


