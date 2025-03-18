using UnityEngine;

public class EnemyNormal : Enemy_Base
{
    private void OnValidate()
    {
        // add enemy components
        if (!GetComponent<EnemyHealth_Base>())
        {
            gameObject.AddComponent<EnemyHealth_Base>();
        }

        if (!GetComponent<EnemyNormalAttack>())
        {
            gameObject.AddComponent<EnemyNormalAttack>();
        }

        if (!GetComponent<EnemyNormalMovement>())
        {
            gameObject.AddComponent<EnemyNormalMovement>();
        }
    }
}
