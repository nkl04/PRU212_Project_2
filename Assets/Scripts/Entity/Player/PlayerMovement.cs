using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 DirectionVector { get; private set; }
    public bool IsMoving { get; private set; }

    private bool isFacingRight = true;

    public void OnMove(Vector2 directionVector)
    {
        DirectionVector = directionVector;
        IsMoving = directionVector.magnitude > 0;

        if (isFacingRight && directionVector.x < 0)
        {
            Flip();
            isFacingRight = false;
        }
        else if (!isFacingRight && directionVector.x > 0)
        {
            Flip();
            isFacingRight = true;
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
