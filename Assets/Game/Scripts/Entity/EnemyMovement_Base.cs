using UnityEngine;

public abstract class EnemyMovement_Base : MonoBehaviour
{
    protected GameObject target;
    protected bool facingRight = true;

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public abstract void Move(float speed);

    protected void Flip(float directionX)
    {
        if ((directionX > 0 && !facingRight) || (directionX < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
