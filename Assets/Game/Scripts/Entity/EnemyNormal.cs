using UnityEngine;

public class EnemyNormal : Enemy_Base
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanFollowPlayer = false;
            CanAttackPlayer = true;
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanFollowPlayer = true;
            CanAttackPlayer = false;
        }
    }

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
