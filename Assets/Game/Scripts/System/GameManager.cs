using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public ConfigLevel SelectedLevel => selectedLevel;
    [SerializeField] private ConfigLevel selectedLevel;


}
