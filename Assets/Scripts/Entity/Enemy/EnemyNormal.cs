using UnityEngine;

public class EnemyNormal : Enemy
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Player"))
        {
            CanFollowPlayer = false;
            CanAttackPlayer = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.CompareTag("Player"))
        {
            CanFollowPlayer = true;
            CanAttackPlayer = false;
        }
    }

    private void OnValidate()
    {
        // add enemy components
        if (!GetComponent<EnemyHealth>())
        {
            gameObject.AddComponent<EnemyHealth>();
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
