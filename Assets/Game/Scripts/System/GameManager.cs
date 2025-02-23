using ControlFreak2;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public ConfigLevel SelectedLevel => selectedLevel;
    [SerializeField] private ConfigLevel selectedLevel;

    private StateMachine<GameState> stateMachine;
    public GameStateSplash GameStateSplash { get; private set; }
    public GameStateMainMenu GameStateMainMenu { get; private set; }
    public GameStateGameplay GameStateGameplay { get; private set; }
    public GameStatePause GameStatePause { get; private set; }
    public GameStateSkillSelection GameStateSkillSelection { get; private set; }
    public GameStateLevelEnd GameStateLevelEnd { get; private set; }
    public GameStatePlaying GameStatePlaying { get; private set; }
    public bool IsPaused { get; set; } = false;
    public bool IsPlayerLevelUp { get; set; } = false;

    private new void Awake()
    {
        base.Awake();

        GameStateSplash = new GameStateSplash(this, stateMachine);
        GameStateMainMenu = new GameStateMainMenu(this, stateMachine);
        GameStateGameplay = new GameStateGameplay(this, stateMachine);
        GameStatePause = new GameStatePause(this, stateMachine);
        GameStateSkillSelection = new GameStateSkillSelection(this, stateMachine);
        GameStateLevelEnd = new GameStateLevelEnd(this, stateMachine);
        GameStatePlaying = new GameStatePlaying(this, stateMachine);

        stateMachine = new StateMachine<GameState>();

        stateMachine.ChangeState(GameStateGameplay);
    }

    private void Update()
    {
        stateMachine.Update();
    }

}
