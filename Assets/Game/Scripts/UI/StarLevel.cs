using UnityEngine;

public class StarLevel : MonoBehaviour
{
    [SerializeField] private Transform[] stars;

    private int starLevel;
    public void SetStarLevel(int level)
    {
        starLevel = level;
        InactiveStars();

        for (int i = 0; i < level; i++)
        {
            stars[i].GetChild(1).gameObject.SetActive(true);
        }
    }


    public void InactiveStars()
    {
        foreach (var item in stars)
        {
            item.GetChild(1).gameObject.SetActive(false);
        }
    }
}
