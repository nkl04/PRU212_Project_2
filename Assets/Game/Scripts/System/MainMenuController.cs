using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public ConfigLevel SelectedConfigLevel { get; set; }

    [SerializeField] private GameObject popUpSelectLevelGameObj;

    private GameManager gameManager;
    private PopUpSelectLevel popUpSelectLevel;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        popUpSelectLevel = popUpSelectLevelGameObj.GetComponent<PopUpSelectLevel>();
        SelectedConfigLevel = gameManager.ConfigLevelHolder.levels[0];
    }

    private void Start()
    {
        popUpSelectLevel.SetUpIcon(gameManager.ConfigLevelIcons.levelIcons.Count);
    }

    public void OnTapPlay()
    {
        //set the selected level to gamemanager
        GameManager.Instance.SelectedLevel = SelectedConfigLevel;
        //transition to the gameplay scene
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
    }

    public void OnTapSelectLevel()
    {

    }
}
