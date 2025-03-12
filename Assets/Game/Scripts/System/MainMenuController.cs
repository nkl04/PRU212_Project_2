using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject popUpSelectLevelGameObj;

    private GameManager gameManager;
    private PopUpSelectLevel popUpSelectLevel;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        popUpSelectLevel = popUpSelectLevelGameObj.GetComponent<PopUpSelectLevel>();
    }

    private void Start()
    {
        popUpSelectLevel.SetUpIcon(gameManager.ConfigLevelIcons.levelIcons.Count);
    }

    public void OnTapPlay()
    {
        //set the selected level to gamemanager

        //transition to the gameplay scene
    }

    public void OnTapSelectLevel()
    {

    }
}
