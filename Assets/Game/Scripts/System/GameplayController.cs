using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
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

    private PopUpSkillSelect _popUpSelectSkill;
    private PopUpPause _popUpPause;
    private void Start()
    {
        _popUpSelectSkill = popUpSelectSkill.GetComponent<PopUpSkillSelect>();
        _popUpPause = popUpPause.GetComponent<PopUpPause>();

        EventHandlers.OnRandomSkillsEvent += UpdatePopUpSkill;
        EventHandlers.OnExpCollectedEvent += UpdateExpBar;
        EventHandlers.OnLevelUpEvent += UpdateLevel;
        EventHandlers.OnSkillSelectedEvent += OnSkillSelected;
    }

    private void OnSkillSelected(Dictionary<ConfigSkill, int> dictionary)
    {
        _popUpSelectSkill.SetCurrentSkillIcons(dictionary);
        _popUpPause.SetCurrentSkillIcons(dictionary);
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
        popUpSelectSkill.gameObject.SetActive(true);

        _popUpSelectSkill.SetSkills(obj);

        Time.timeScale = 0;
    }
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
