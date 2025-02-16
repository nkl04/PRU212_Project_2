using UnityEngine;

public class Diamond : Item
{
    [Header("Diamond Info")]
    [SerializeField] private int expValue = 1;
    protected override void Action(PlayerController playerController)
    {
        playerController.PlayerStats.Exp += expValue;
    }
}
