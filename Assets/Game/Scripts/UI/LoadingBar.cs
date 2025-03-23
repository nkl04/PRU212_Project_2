using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI loadingNumberText;

    private void Start()
    {
        fill.fillAmount = 0;
        StartCoroutine(LoadingBarCoroutine());
    }

    private IEnumerator LoadingBarCoroutine()
    {
        // incremental loading
        while (fill.fillAmount < 1)
        {
            fill.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }

        // loading done
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    private void Update()
    {
        loadingNumberText.text = (fill.fillAmount * 100).ToString("F0") + "%";
    }
}
