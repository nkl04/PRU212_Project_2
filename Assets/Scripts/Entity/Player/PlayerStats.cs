using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    public float Exp
    {
        get => exp; set
        {
            exp = value;
            EventHandlers.CallOnExpCollectedEvent(exp);
        }
    }

    private float exp = 0;
}
