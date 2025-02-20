using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public LevelSO SelectedLevel => selectedLevel;
    [SerializeField] private LevelSO selectedLevel;


}
