using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private StarLevel starLevel;

    [SerializeField] private Button Button;

    public void SetSkill(ConfigSkill configSkill, int level)
    {
        skillName.text = configSkill.skillName;
        iconImage.sprite = configSkill.SkillLevelList[level].icon;
        description.text = configSkill.SkillLevelList[level].description;
        starLevel.SetStarLevel(level);

        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() =>
        {
            SkillManager.Instance.LevelUpSkill(configSkill);
        });
    }
}
