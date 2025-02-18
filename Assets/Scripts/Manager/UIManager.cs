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
    }

    private void UpdateExpBar(float exp)
    {
        // exp max based on the level of player
        // demo value is 10
        float fillAmount = exp / 10;
        expBar.SetFillAmount(fillAmount);
    }
}
