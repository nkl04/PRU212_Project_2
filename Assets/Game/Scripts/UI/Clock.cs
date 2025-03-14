using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clock : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private GameObject toolTest;
    public void OnPointerClick(PointerEventData eventData)
    {
        toolTest.SetActive(!toolTest.activeSelf);
    }

    public void UpdateClock(int minutes, int seconds)
    {
        clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
