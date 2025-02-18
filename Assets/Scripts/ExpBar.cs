using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI levelText;

    public void SetFillAmount(float amount)
    {
        if (fill == null) return;
        fill.fillAmount = amount;
    }

    public void SetLevelText(int level)
    {
        if (levelText == null) return;
        levelText.text = "Level " + level.ToString();
    }
}
