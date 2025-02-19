using TMPro;
using UnityEngine;

public class GoldCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCountText;
    private int goldCount = 0;
    private void OnGoldSelected()
    {
        goldCountText.text = (++goldCount).ToString();
    }
}
