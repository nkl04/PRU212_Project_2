using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button pauseBtn;

    [Header("PopUps")]
    [SerializeField] private Transform popUpSelectSkill;
    [SerializeField] private Transform popUpPause;
    [SerializeField] private Transform popUpWin;
    [SerializeField] private Transform popUpGameOver;

    [Header("Elements")]
    [SerializeField] private Clock clock;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private ExpBar expBar;

    public void OnTapPause()
    {
        GameManager.Instance.UpdateGameState(GameState.Pause);
        popUpPause.gameObject.SetActive(true);

        Debug.Log("<color=orange>=> PAUSE <=</color>");
    }

    public void OnTapResume()
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
        popUpPause.gameObject.SetActive(false);

        Debug.Log("<color=cyan>=> RESUME <=</color>");
    }



}
