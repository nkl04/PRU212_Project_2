using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public ConfigLevel SelectedConfigLevel { get; set; }

    [SerializeField] private GameObject popUpSelectLevelGameObj;
    [SerializeField] private GameObject popUpSettingsGameObj;
    [SerializeField] private Transform fadeAnimTransform;
    private GameManager gameManager;
    private PopUpSelectLevel popUpSelectLevel;
    private PopUpSettings popUpSettings;

    [Header("Current Level Info")]
    [SerializeField] private TextMeshProUGUI currentLevelNameText;
    [SerializeField] private TextMeshProUGUI longestSurvivedText;
    [SerializeField] private Image levelIconImage;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        popUpSelectLevel = popUpSelectLevelGameObj.GetComponent<PopUpSelectLevel>();
        popUpSettings = popUpSettingsGameObj.GetComponent<PopUpSettings>();

        popUpSettingsGameObj.SetActive(false);
        popUpSelectLevelGameObj.SetActive(false);

        SelectedConfigLevel = gameManager.SelectedLevel;
        SetUpSelectLevelMainMenu(SelectedConfigLevel);

        fadeAnimTransform.gameObject.SetActive(true);
        fadeAnimTransform.GetComponent<FadeAnimation>().FadeOut(() => fadeAnimTransform.gameObject.SetActive(false));
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

    public void OnTapSound()
    {
        AudioManager.Instance.MuteSound();
    }

    public void OnTapMusic()
    {
        AudioManager.Instance.MuteMusic();
    }

    public void SetUpSelectLevelMainMenu(ConfigLevel configLevel)
    {
        currentLevelNameText.text = $"{configLevel.levelIndex + 1}.{configLevel.levelName}";

        (int, int) time = GameManager.Instance.GetBestTimeInLevel(configLevel);

        if (GameManager.Instance.IsFinishLevel(configLevel.levelIndex))
        {
            longestSurvivedText.text = "Finished";
        }
        else
        {

            if (time.Item1 == 0 && time.Item2 == 0)
            {
                longestSurvivedText.text = "";
            }
            else
                longestSurvivedText.text = $"Longest Survived: {Utilities.FormatTime(time.Item1, time.Item2)}";
        }

        levelIconImage.sprite = GameManager.Instance.ConfigLevelIcons.levelIcons[configLevel.levelIndex];
    }
}
