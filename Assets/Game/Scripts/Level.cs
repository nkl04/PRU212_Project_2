using DG.Tweening;
using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backGround;
    [SerializeField] private bool isDebug;
    private ConfigLevel _configLevel;

    private void Awake()
    {
        if (isDebug)
        {
            _configLevel = GameManager.Instance.ConfigLevelHolder.levels[0];
        }
        else
            _configLevel = GameManager.Instance.SelectedLevel;

        _backGround.sprite = _configLevel.background;
    }
    private void Start()
    {
        EventHandlers.CallOnGameStartEvent(_configLevel);
    }
}

