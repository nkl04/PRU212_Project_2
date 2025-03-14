using TMPro;
using UnityEngine;

public class PopUpLose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clock_Text;
    [SerializeField] private TextMeshProUGUI chapter_Text;
    [SerializeField] private TextMeshProUGUI bestTime_Text;
    [SerializeField] private TextMeshProUGUI killCount;

    public void SetData((int, int) clock, string chapter, (int, int) bestTime, int killCount)
    {
        clock_Text.text = string.Format("{0:00}:{1:00}", clock.Item1, clock.Item2);
        chapter_Text.text = chapter;
        bestTime_Text.text = "Best:" + string.Format("{0:00}:{1:00}", bestTime.Item1, bestTime.Item2);
        this.killCount.text = killCount.ToString();
    }
}
