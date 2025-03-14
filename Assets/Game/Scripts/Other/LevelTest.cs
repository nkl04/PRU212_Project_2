using UnityEngine;
using UnityEngine.UI;

public class LevelTest : MonoBehaviour
{
    [SerializeField] private Button winBtn;
    [SerializeField] private Button loseBtn;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        winBtn.onClick.AddListener(WinLevel);
        loseBtn.onClick.AddListener(LoseLevel);
    }

    private void LoseLevel()
    {
        playerController.PlayerHealth.TakeDamage(100000000000);
    }

    private void WinLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.Win);
    }
}
