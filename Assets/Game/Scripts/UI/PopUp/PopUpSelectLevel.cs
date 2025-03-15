using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSelectLevel : MonoBehaviour
{
    private ConfigLevel selectedConfigLevel;
    [SerializeField] private Transform popUpInfo;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [Space(10)]
    [SerializeField] private GameObject scrollBar;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private GameObject levelIconPrefab;
    [SerializeField] private Button selectButton;

    private float[] pos;
    private float scrollPos;
    private int selectedIndex;
    private MainMenuController mainMenuController;
    private List<bool> activeStatusList;
    float distance;
    private void Awake()
    {
        mainMenuController = FindFirstObjectByType<MainMenuController>();
        selectedIndex = GameManager.Instance.SelectedLevel.levelIndex;
    }

    private void Start()
    {
        selectButton.onClick.AddListener(() =>
        {
            selectedConfigLevel = GameManager.Instance.ConfigLevelHolder.levels[selectedIndex];
            mainMenuController.SelectedConfigLevel = selectedConfigLevel;
            mainMenuController.SetUpSelectLevelMainMenu(selectedConfigLevel);
            gameObject.SetActive(false);
            GameManager.Instance.SelectedLevel = selectedConfigLevel;
            GameManager.Instance.SaveData();
        });

        InitializeScrollPosition();
    }

    public void SetUp(Dictionary<ConfigLevel, bool> levelDictionary, List<Sprite> iconList)
    {
        activeStatusList = new List<bool>();
        bool prevStatus = true;

        for (int i = 0; i < levelDictionary.Count; i++)
        {
            activeStatusList.Add(prevStatus);
            GameObject levelIcon = Instantiate(levelIconPrefab, levelContainer);
            levelIcon.GetComponent<LevelIcon>().SetImageVisual(iconList[i]);
            levelIcon.GetComponent<LevelIcon>().SetActive(prevStatus);
            prevStatus = levelDictionary[GameManager.Instance.ConfigLevelHolder.levels[i]];
        }

        InitializeScrollPosition();
    }

    public void SetSelectedIndexInfo()
    {
        levelNameText.text = $"{GameManager.Instance.ConfigLevelHolder.levels[selectedIndex].levelIndex + 1}. {GameManager.Instance.ConfigLevelHolder.levels[selectedIndex].levelName}";

        descriptionText.text = GameManager.Instance.ConfigLevelHolder.levels[selectedIndex].description;

        popUpInfo.gameObject.SetActive(true);
    }

    private void InitializeScrollPosition()
    {
        int childCount = levelContainer.childCount;
        if (childCount > 1)
        {
            pos = new float[childCount];
            distance = 1f / (childCount - 1);

            for (int i = 0; i < childCount; i++)
            {
                pos[i] = distance * i;
            }

            scrollPos = pos[selectedIndex];
            scrollBar.GetComponent<Scrollbar>().value = scrollPos;
        }
    }

    private void Update()
    {
        if (pos == null || pos.Length == 0)
            return;

        bool isScrolling = Input.GetMouseButton(0);

        if (isScrolling)
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
            popUpInfo.gameObject.SetActive(false);
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
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                levelContainer.GetChild(i).localScale = Vector2.Lerp(levelContainer.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                selectedIndex = i;
                selectButton.gameObject.SetActive(activeStatusList[i]);

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        levelContainer.GetChild(j).localScale = Vector2.Lerp(levelContainer.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }

                if (!isScrolling)
                {
                    SetSelectedIndexInfo();
                }
            }
        }
    }

}

