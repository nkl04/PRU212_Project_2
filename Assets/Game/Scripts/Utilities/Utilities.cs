using Unity.VisualScripting;
using UnityEngine;

public class Utilities
{
    public static float CurrentMillis()
    {
        return Time.time * 1000;
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
}
