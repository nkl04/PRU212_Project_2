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
}
