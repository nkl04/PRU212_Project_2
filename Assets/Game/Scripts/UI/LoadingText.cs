using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingDotText;

    private void Start()
    {
        StartCoroutine(LoadingTextCoroutine());
    }

    IEnumerator LoadingTextCoroutine()
    {
        while (true)
        {
            loadingDotText.text = ".";
            yield return new WaitForSeconds(0.3f);
            loadingDotText.text = "..";
            yield return new WaitForSeconds(0.3f);
            loadingDotText.text = "...";
            yield return new WaitForSeconds(0.3f);
        }
    }
}
