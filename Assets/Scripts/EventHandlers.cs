using System;
using UnityEngine;

public class EventHandlers
{
    public static event Action<float> OnExpCollectedEvent;

    public static void CallOnExpCollectedEvent(float exp)
    {
        OnExpCollectedEvent?.Invoke(exp);
    }

    public static event Action<Enemy> OnEnemyDeadEvent;
    public static void CallOnEnemyDeadEvent(Enemy enemy)
    {
        OnEnemyDeadEvent?.Invoke(enemy);
    }



}
