using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private KillCount killCount;

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

    private void Start()
    {
        EventHandlers.OnRandomSkillsEvent += UpdatePopUpSkill;
        EventHandlers.OnExpCollectedEvent += UpdateExpBar;
        EventHandlers.OnLevelUpEvent += UpdateLevel;
        EventHandlers.OnSkillSelectedEvent += OnSkillSelected;
        EventHandlers.OnPlayerDeadEvent += OnPlayerDead;
    }
    private void OnSkillSelected(Dictionary<ConfigSkill, int> dictionary)
    {
        popUpSelectSkillTransform.GetComponent<PopUpSkillSelect>().SetCurrentSkillIcons(dictionary);
        popUpPauseTransform.GetComponent<PopUpPause>().SetCurrentSkillIcons(dictionary);
    }

    private void UpdateLevel(int level)
    {
        expBar.SetLevelText(level);
    }

    private void UpdateExpBar(float exp, float maxExp)
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
    public void OnTapPause()
    {
        GameManager.Instance.UpdateGameState(GameState.Pause);
        popUpPauseTransform.gameObject.SetActive(true);

        Debug.Log("<color=orange>=> PAUSE <=</color>");
    }
    public void OnTapResume()
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
        popUpPauseTransform.gameObject.SetActive(false);

        Debug.Log("<color=cyan>=> RESUME <=</color>");
    }

    public void OnTapConfirm()
    {

    }

    private void OnPlayerDead()
    {
        GameManager.Instance.UpdateGameState(GameState.End);
        PopUpLose popUpLose = popUpLoseTransform.GetComponent<PopUpLose>();
        popUpLose.SetData(timeManager.GetTime(), "Chapter 1", 0, killCount.GetKillCount());
        popUpLoseTransform.gameObject.SetActive(true);
    }




}
