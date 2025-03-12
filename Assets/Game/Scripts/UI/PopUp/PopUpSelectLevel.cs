using DG.Tweening.Core.Easing;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PopUpSelectLevel : MonoBehaviour
{
    private ConfigLevel selectedConfigLevel;
    [SerializeField] private GameObject scrollBar;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private GameObject levelIconPrefab;
    private float[] pos;
    private float scrollPos;

    private void Start()
    {

    }

    public void SetUpIcon(int levelAmount)
    {
        for (int i = 0; i < levelAmount; i++)
        {
            GameObject levelIcon = Instantiate(levelIconPrefab, levelContainer);
        }
    }

    private void Update()
    {
        pos = new float[levelContainer.childCount];
        float distance = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                levelContainer.GetChild(i).localScale = Vector2.Lerp(levelContainer.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        levelContainer.GetChild(j).localScale = Vector2.Lerp(levelContainer.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
    }
}
