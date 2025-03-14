using UnityEngine;
using UnityEngine.UI;

public class PopUpSettings : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private GameObject soundIconActive;
    [SerializeField] private GameObject soundIconInactive;

    [SerializeField] private GameObject musicIconActive;
    [SerializeField] private GameObject musicIconInactive;

    [SerializeField] private Sprite activeButton;
    [SerializeField] private Sprite inactiveButton;

    public void Start()
    {
        soundButton.onClick.AddListener(() =>
        {
            soundIconActive.SetActive(!soundIconActive.activeSelf);
            soundIconInactive.SetActive(!soundIconInactive.activeSelf);
            soundButton.image.sprite = soundIconActive.activeSelf ? activeButton : inactiveButton;
        });

        musicButton.onClick.AddListener(() =>
        {
            musicIconActive.SetActive(!musicIconActive.activeSelf);
            musicIconInactive.SetActive(!musicIconInactive.activeSelf);
            musicButton.image.sprite = musicIconActive.activeSelf ? activeButton : inactiveButton;
        });
    }


}
