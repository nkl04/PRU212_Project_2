using UnityEngine;

public class RoninOyoroiSkillController : SupplySkillController
{
    private float damageReductionMultiplier = 0f;
    public override void ExecuteLevel(int level)
    {
        switch (level)
        {
            case 1:
                damageReductionMultiplier = 0.1f;
                break;
            case 2:
                damageReductionMultiplier = 0.2f;
                break;
            case 3:
                damageReductionMultiplier = 0.3f;
                break;
            case 4:
                damageReductionMultiplier = 0.4f;
                break;
            case 5:
                damageReductionMultiplier = 0.5f;
                break;
        }
        PlayerController.PlayerStats.DamageReduction = damageReductionMultiplier;
    }
}
