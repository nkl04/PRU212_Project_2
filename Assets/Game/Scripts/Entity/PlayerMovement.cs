using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject directionVectorUI;

    private void Start()
    {
        directionVectorUI.SetActive(false);
    }
    public Vector2 DirectionVector { get; private set; }
    public bool IsMoving { get; private set; }

    private bool isFacingRight = true;

    public void OnMove(Vector2 directionVector)
    {
        DirectionVector = directionVector.normalized;
        IsMoving = directionVector.magnitude > 0;

        #region Direction Vector UI
        if (IsMoving)
        {
            directionVectorUI.SetActive(true);
            directionVectorUI.transform.up = directionVector.normalized;
        }
        else
        {
            directionVectorUI.SetActive(false);
        }
        #endregion

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
