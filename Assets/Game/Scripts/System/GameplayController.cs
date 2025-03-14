using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    private ConfigLevel configLevel;
    private Wave currentWave;
    private Wave nextWave;
    private int minutes;
    private int seconds;
    private float eslapsedTime;
    private bool isPaused = false;
    private bool isFinished = false;
    private EnemyManager enemyManager;

    [SerializeField] private KillCount killCount;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Buttons")]
    [SerializeField] private Button pauseBtn;

    [Header("PopUps")]
    [SerializeField] private Transform popUpSelectSkillTransform;
    [SerializeField] private Transform popUpPauseTransform;
    [SerializeField] private Transform popUpWinTransform;
    [SerializeField] private Transform popUpLoseTransform;

    [Header("Elements")]
    [SerializeField] private Clock clock;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private ExpBar expBar;

    [Header("Animation")]
    [SerializeField] private Transform fadeAnimTransform;

    private void Awake()
    {
        EventHandlers.OnGameStartEvent += OnGameStart;
        EventHandlers.OnRandomSkillsEvent += UpdatePopUpSkill;
        EventHandlers.OnExpCollectedEvent += UpdateExpBar;
        EventHandlers.OnLevelUpEvent += UpdateLevel;
        EventHandlers.OnSkillSelectedEvent += UpdateSelectSkillPopUp;
        EventHandlers.OnGameStateUpdateEvent += EventHandlers_OnGameStateUpdateEvent;

        enemyManager = FindFirstObjectByType<EnemyManager>();

        popUpLoseTransform.gameObject.SetActive(false);
        popUpWinTransform.gameObject.SetActive(false);
        popUpSelectSkillTransform.gameObject.SetActive(false);
        popUpPauseTransform.gameObject.SetActive(false);

    }


    private void OnDestroy()
    {
        EventHandlers.OnRandomSkillsEvent -= UpdatePopUpSkill;
        EventHandlers.OnExpCollectedEvent -= UpdateExpBar;
        EventHandlers.OnLevelUpEvent -= UpdateLevel;
        EventHandlers.OnSkillSelectedEvent -= UpdateSelectSkillPopUp;
        EventHandlers.OnGameStartEvent -= OnGameStart;
        EventHandlers.OnGameStateUpdateEvent -= EventHandlers_OnGameStateUpdateEvent;
    }

    private void Start()
    {
        fadeAnimTransform.gameObject.SetActive(true);
        fadeAnimTransform.GetComponent<FadeAnimation>().FadeOut();
    }

    private void Update()
    {
        if (isPaused || isFinished) return;

        UpdateTime();

        if (currentWave != null)
        {
            if (eslapsedTime >= currentWave.startTime)
            {
                enemySpawner.SetWave(currentWave);

                int index = configLevel.GetWaveIndex(currentWave);
                if (index < configLevel.waveList.Count - 1)
                {
                    currentWave = configLevel.GetWave(index + 1);
                }
                else
                {
                    //Out of wave
                    Debug.Log("No more wave");
                    currentWave = null;
                }
            }
        }

        if (currentWave == null && enemyManager.IsClearEnemies())
        {
            GameManager.Instance.UpdateGameState(GameState.Win);
        }
    }


    private void OnGameStart(ConfigLevel configLevel)
    {
        this.configLevel = configLevel;
        if (this.configLevel != null)
        {
            currentWave = configLevel.GetWave(0);
        }
    }

    private void EventHandlers_OnGameStateUpdateEvent(GameState state)
    {
        if (state == GameState.Win)
        {
            isFinished = true;
            UpdateWinPopUp();
        }
        else if (state == GameState.End)
        {
            if (GameManager.Instance.IsBestTimeInLevel(configLevel, (minutes, seconds)))
            {
                GameManager.Instance.UpdateBestTimeInLevel(configLevel, (minutes, seconds));
            }

            UpdateLosePopUp();
        }
    }

    private void UpdateSelectSkillPopUp(Dictionary<ConfigSkill, int> dictionary)
    {
        popUpSelectSkillTransform.GetComponent<PopUpSkillSelect>().SetCurrentSkillIcons(dictionary);
        popUpPauseTransform.GetComponent<PopUpPause>().SetCurrentSkillIcons(dictionary);
    }
    private void UpdateLevel(int level)
    {
        expBar.SetLevelText(level);
    }
    private void UpdateExpBar(float exp, float maxExp, Transform p)
    {
        // exp max based on the level of player
        float fillAmount = exp / maxExp;
        expBar.SetFillAmount(fillAmount);
    }
    private void UpdatePopUpSkill((ConfigSkill, int)[] obj)
    {
        popUpSelectSkillTransform.gameObject.SetActive(true);

        popUpSelectSkillTransform.GetComponent<PopUpSkillSelect>().SetSkills(obj);

        Time.timeScale = 0;
    }
    private void UpdateLosePopUp()
    {
        PopUpLose popUpLose = popUpLoseTransform.GetComponent<PopUpLose>();
        popUpLose.SetData((minutes, seconds),
                            "Chapter " + configLevel.levelIndex + 1,
                            GameManager.Instance.GetBestTimeInLevel(configLevel),
                            killCount.GetKillCount());
        popUpLoseTransform.gameObject.SetActive(true);
    }

    public void UpdateWinPopUp()
    {
        PopUpWin popUpWin = popUpWinTransform.GetComponent<PopUpWin>();
        popUpWin.SetData(configLevel.levelIndex + 1, killCount.GetKillCount());
        popUpWinTransform.gameObject.SetActive(true);
    }
    public void OnTapPause()
    {
        Time.timeScale = 0;
        isPaused = true;
        popUpPauseTransform.gameObject.SetActive(true);

        Debug.Log("<color=orange>=> PAUSE <=</color>");
    }
    public void OnTapResume()
    {
        Time.timeScale = 1;
        isPaused = false;
        popUpPauseTransform.gameObject.SetActive(false);

        Debug.Log("<color=cyan>=> RESUME <=</color>");
    }
    public void OnTapConfirm()
    {
        // collect item

        //fade animation
        fadeAnimTransform.gameObject.SetActive(true);
        fadeAnimTransform.GetComponent<FadeAnimation>().FadeIn(() =>
        {
            // redirect to main menu
            GameManager.Instance.UpdateGameState(GameState.MainMenu);
        });
    }

    public void OnTapMuteSound()
    {
        AudioManager.Instance.MuteSound();
    }
    public void OnTapMuteMusic()
    {
        AudioManager.Instance.MuteMusic();
    }

    public void OnTapHome()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
    public void UpdateTime()
    {
        eslapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(eslapsedTime / 60);
        seconds = Mathf.FloorToInt(eslapsedTime % 60);
        clock.UpdateClock(minutes, seconds);
    }
}
