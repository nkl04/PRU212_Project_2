using UnityEngine;

public class Meat : Item
{
    protected override void Action(PlayerController playerController)
    {
        playerController.PlayerHealth.RestoreFullHealth();
    }
}
