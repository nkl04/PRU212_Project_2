using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void SetFillAmount(float amount)
    {
        if (fill == null) return;
        fill.fillAmount = amount;
    }

}
