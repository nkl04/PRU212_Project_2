using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;

    public void UpdateClock(int minutes, int seconds)
    {
        clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
