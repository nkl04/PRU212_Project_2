using ControlFreak2;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public ConfigLevel SelectedLevel => selectedLevel;
    [SerializeField] private ConfigLevel selectedLevel;
    public bool IsPaused { get; set; } = false;
    public bool IsEndLevel { get; set; } = false;
    public bool IsPlayerLevelUp { get; set; } = false;

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
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    End,
    Splash
}
