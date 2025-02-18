using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private ExpBar expBar;
    private PlayerController playerController;

    private void Start()
    {
        expBar = FindFirstObjectByType<ExpBar>();
        playerController = FindFirstObjectByType<PlayerController>();

        EventHandlers.OnExpCollectedEvent += UpdateExpBar;
        EventHandlers.OnLevelUpEvent += UpdateLevel;
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
}
