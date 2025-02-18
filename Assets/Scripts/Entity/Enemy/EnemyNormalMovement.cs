using UnityEngine;

[RequireComponent(typeof(EnemyNormal))]
public class EnemyNormalMovement : EnemyMovement
{
    public override void Move(float speed)
    {
        if (!target) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Flip(direction.x);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
