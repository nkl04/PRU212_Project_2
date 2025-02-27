using System;
using System.Collections.Generic;
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
    //===================================================SKILL========================================================//
    public static event Action<(ConfigSkill, int)[]> OnRandomSkillsEvent;
    public static void CallOnRandomSkillsEvent((ConfigSkill, int)[] configskill_level_pair)
    {
        OnRandomSkillsEvent?.Invoke(configskill_level_pair);
    }

    public static event Action<Dictionary<ConfigSkill, int>> OnSkillSelectedEvent;

    public static void CallOnSkillSelectedEvent(Dictionary<ConfigSkill, int> dictionary)
    {
        OnSkillSelectedEvent?.Invoke(dictionary);
    }

    //===================================================ENEMY========================================================//
    public static event Action<Enemy_Base> OnEnemyDeadEvent;
    public static void CallOnEnemyDeadEvent(Enemy_Base enemy)
    {
        OnEnemyDeadEvent?.Invoke(enemy);
    }


}
