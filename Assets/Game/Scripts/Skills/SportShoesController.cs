using UnityEngine;

public class SportShoesController : SupplySkillController
{
    float additionalSpeed;
    public override void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                additionalSpeed = 0.1f;
                break;
            case 2:
                additionalSpeed = 0.2f;
                break;
            case 3:
                additionalSpeed = 0.3f;
                break;
            case 4:
                additionalSpeed = 0.4f;
                break;
            case 5:
                additionalSpeed = 0.5f;
                break;
        }

        PlayerController.PlayerStats.MoveSpeed += additionalSpeed * PlayerController.PlayerStats.MoveSpeed;
    }

}
