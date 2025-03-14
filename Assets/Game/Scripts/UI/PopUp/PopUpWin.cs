using TMPro;
using UnityEngine;

public class PopUpWin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private TextMeshProUGUI killText;

    public void SetData(int chapterIndex, int kill)
    {
        chapterText.text = "Chapter " + chapterIndex;
        killText.text = "Kill: " + kill;
    }
}
