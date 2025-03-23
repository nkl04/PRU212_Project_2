using System.Collections;
using UnityEngine;

public class EnergyDrinkSkillController : SupplySkillController
{
    private float restoreHealthMultiplier = 0f;
    public override void ExecuteLevel(int level)
    {
        StopAllCoroutines();
        switch (level)
        {
            case 1:
                restoreHealthMultiplier = 0.1f;
                break;
            case 2:
                restoreHealthMultiplier = 0.2f;
                break;
            case 3:
                restoreHealthMultiplier = 0.3f;
                break;
            case 4:
                restoreHealthMultiplier = 0.4f;
                break;
            case 5:
                restoreHealthMultiplier = 0.5f;
                break;
        }
        StartCoroutine(RestoreHealth());
    }

    IEnumerator RestoreHealth()
    {
        while (true)
        {
            PlayerController.PlayerHealth.RestoreHealth(restoreHealthMultiplier);
            yield return new WaitForSeconds(5f);
        }
    }
}
