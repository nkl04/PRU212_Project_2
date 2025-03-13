using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public ConfigLevel SelectedConfigLevel { get; set; }

    [SerializeField] private GameObject popUpSelectLevelGameObj;
    [SerializeField] private Transform fadeAnimTransform;
    private GameManager gameManager;
    private PopUpSelectLevel popUpSelectLevel;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        popUpSelectLevel = popUpSelectLevelGameObj.GetComponent<PopUpSelectLevel>();
        fadeAnimTransform.gameObject.SetActive(false);
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
        // fadein the fade animation
        fadeAnimTransform.gameObject.SetActive(true);
        fadeAnimTransform.GetComponent<FadeAnimation>()
            .FadeIn(() =>
            //transition to the gameplay scene
            GameManager.Instance.UpdateGameState(GameState.Gameplay));
    }

    public void OnTapSelectLevel()
    {

    }
}
