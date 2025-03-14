using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSelectLevel : MonoBehaviour
{
    private ConfigLevel selectedConfigLevel;
    [SerializeField] private GameObject scrollBar;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private GameObject levelIconPrefab;
    [SerializeField] private Button selectButton;

    private float[] pos;
    private float scrollPos;
    private int selectedIndex;
    private MainMenuController mainMenuController;
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

    public void SetUpIcon(int levelAmount)
    {
        for (int i = 0; i < levelAmount; i++)
        {
            GameObject levelIcon = Instantiate(levelIconPrefab, levelContainer);
            levelIcon.GetComponent<Image>().sprite = GameManager.Instance.ConfigLevelIcons.levelIcons[i];
        }

        InitializeScrollPosition();
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
                selectedIndex = i;
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

