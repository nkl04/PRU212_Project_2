using System;
using UnityEngine;
using ControlFreak2;

public class GameInput : MonoBehaviour
{
    public event Action<Vector2> OnMovePerformed;
    public event Action<Vector2> OnMoveCanceled;
    private Vector2 lastMoveInput;

    private void Update()
    {
        Vector2 moveInput = new Vector2(CF2Input.GetAxis("Horizontal"), CF2Input.GetAxis("Vertical"));
        if (moveInput.magnitude > 0)
        {
            OnMovePerformed?.Invoke(moveInput);
        }
        else if (moveInput == Vector2.zero && lastMoveInput != Vector2.zero)
        {
            OnMoveCanceled?.Invoke(Vector2.zero);
        }
        lastMoveInput = moveInput;
    }
}
