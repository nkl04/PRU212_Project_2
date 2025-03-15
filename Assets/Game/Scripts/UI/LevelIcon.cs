using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    public bool IsActive => _activeVisual.activeSelf;
    [SerializeField] private GameObject _activeVisual;
    [SerializeField] private GameObject _inactiveVisual;

    public void SetImageVisual(Sprite sprite)
    {
        _activeVisual.GetComponent<Image>().sprite = sprite;
        _inactiveVisual.GetComponent<Image>().sprite = sprite;
    }

    public void SetActive(bool active)
    {
        _activeVisual.SetActive(active);
        _inactiveVisual.SetActive(!active);
    }
}
