using System;
using UnityEngine;

public class EventHandlers
{
    public static event Action<float, float> OnExpCollectedEvent;

    public static void CallOnExpCollectedEvent(float exp, float maxExp)
    {
        OnExpCollectedEvent?.Invoke(exp, maxExp);
    }

    public static event Action<Enemy_Base> OnEnemyDeadEvent;
    public static void CallOnEnemyDeadEvent(Enemy_Base enemy)
    {
        OnEnemyDeadEvent?.Invoke(enemy);
    }

    public static event Action<int> OnLevelUpEvent;
    public static void CallOnLevelUpEvent(int level)
    {
        OnLevelUpEvent?.Invoke(level);
    }



}
