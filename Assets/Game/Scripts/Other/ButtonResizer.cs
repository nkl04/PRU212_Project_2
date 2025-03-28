using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonResizer : MonoBehaviour
{
    [SerializeField] private Sprite selectedButtonSprite;
    [SerializeField] private Sprite normalButtonSprite;

    public RectTransform container;
    public Button[] buttons;
    public float expandedRatio = 0.4f;
    public float resizeDuration = 0.3f;
    private int selectedIndex = 1;

    private float selectedChildSize = 1.5f;
    private float normalChildSize = 1f;
    private float selectedChildTransformY = 40f;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SetSelectedIndex(index));
        }

        UpdateButtonSizes();
    }

    public void SetSelectedIndex(int index)
    {
        if (selectedIndex == index) return;

        selectedIndex = index;
        UpdateButtonSizes();
    }

    void UpdateButtonSizes()
    {
        float totalWidth = container.rect.width;
        float selectedWidth = totalWidth * expandedRatio;
        float normalWidth = (totalWidth - selectedWidth) / (buttons.Length - 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rect = buttons[i].GetComponent<RectTransform>();
            RectTransform child = rect.GetChild(0).GetComponent<RectTransform>();
            Image image = buttons[i].GetComponent<Image>();
            image.sprite = (i == selectedIndex) ? selectedButtonSprite : normalButtonSprite;
            float targetWidth = (i == selectedIndex) ? selectedWidth : normalWidth;
            rect.DOSizeDelta(new Vector2(targetWidth, rect.sizeDelta.y), resizeDuration);
            float targetChildSize = (i == selectedIndex) ? selectedChildSize : normalChildSize;
            float targetChildTransformY = (i == selectedIndex) ? selectedChildTransformY : 0f;
            child.DOScale(targetChildSize, resizeDuration);
            child.DOLocalMoveY(targetChildTransformY, resizeDuration);
        }
    }
}
