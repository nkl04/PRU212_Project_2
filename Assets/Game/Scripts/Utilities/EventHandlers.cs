using System;
using UnityEngine;

public class EventHandlers
{
    //===================================================SYSTEM========================================================//
    public static event Action<GameState> OnGameStateUpdateEvent;

    public static void CallOnGameStateUpdateEvent(GameState gamestate)
    {
        OnGameStateUpdateEvent?.Invoke(gamestate);
    }

    //===================================================PLAYER========================================================//
    public static event Action<float, float> OnExpCollectedEvent;
    public static void CallOnExpCollectedEvent(float exp, float maxExp)
    {
        OnExpCollectedEvent?.Invoke(exp, maxExp);
    }


    public static event Action<int> OnLevelUpEvent;
    public static void CallOnLevelUpEvent(int level)
    {
        OnLevelUpEvent?.Invoke(level);
    }

    //===================================================ENEMY========================================================//
    public static event Action<Enemy_Base> OnEnemyDeadEvent;
    public static void CallOnEnemyDeadEvent(Enemy_Base enemy)
    {
        OnEnemyDeadEvent?.Invoke(enemy);
    }


}
