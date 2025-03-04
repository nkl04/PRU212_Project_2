using Unity.VisualScripting;
using UnityEngine;

public class Utilities
{
    public static float CurrentMillis()
    {
        return Time.time * 1000;
    }

    public static string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return FormatTime(minutes, seconds);
    }

    public static string FormatTime(int time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return FormatTime(minutes, seconds);
    }

    public static string FormatTime(int minutes, int seconds)
    {
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public static class Tag
    {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string Projectile = "Projectile";
    }

    public static class AnimationClips
    {
        public class Player
        {
            public const string Idle = "Player_Idle";
            public const string Run = "Player_Run";
            public const string Die = "Player_Die";
        }

        public class Enemy
        {
            public const string Run = "Run";
            public const string Die = "Die";
        }
    }

    public static class Scene
    {
        public const string MainMenu = "MainMenuScene";
        public const string Gameplay = "GameplayScene";
        public const string LevelSelection = "SplashScene";
    }
}
