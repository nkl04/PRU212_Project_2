using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class StarLevel : MonoBehaviour
{
    [SerializeField] private Transform[] stars;

    [SerializeField] private bool hasAnimation;
    private int starLevel;
    public void SetStarLevel(int level)
    {
        starLevel = level;
        InactiveStars();

        for (int i = 0; i < level; i++)
        {
            stars[i].GetChild(1).gameObject.SetActive(true);
        }
        if (hasAnimation)
        {
            StarAnimation(stars[starLevel - 1].GetChild(1));
        }
    }

    public void StarAnimation(Transform starActiveVisual)
    {
        Image image = starActiveVisual.GetComponent<Image>();

        Color color = image.color;
        color.a = 1f;
        image.color = color;

        image.DOFade(0f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
    }


    public void InactiveStars()
    {
        foreach (var item in stars)
        {
            item.GetChild(1).gameObject.SetActive(false);
        }
    }
}
