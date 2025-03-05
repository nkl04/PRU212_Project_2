using DG.Tweening;
using System;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] private ConfigLevel _configLevel;
    [Space(10)]
    [SerializeField] private SpriteRenderer _backGround;

    private void Start()
    {
        EventHandlers.CallOnGameStartEvent(_configLevel);
        _backGround.sprite = _configLevel.background;
    }
}

