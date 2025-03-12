using DG.Tweening;
using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backGround;
    private ConfigLevel _configLevel;

    private void Awake()
    {
        _configLevel = GameManager.Instance.SelectedLevel;
        _backGround.sprite = _configLevel.background;
    }
    private void Start()
    {
        EventHandlers.CallOnGameStartEvent(_configLevel);
    }
}

