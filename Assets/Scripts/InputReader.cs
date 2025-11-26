using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";

    public float HorizontalDirection { get; private set; }
    public float VerticalDirection { get; private set; }

    public event Action OnJumpHoldStart;
    public event Action OnJumpHoldEnd;

    private void Update()
    {
        HorizontalDirection = Input.GetAxis(Horizontal);
        VerticalDirection = Input.GetAxis(Vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpHoldStart?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpHoldEnd?.Invoke();
        }
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }
}
