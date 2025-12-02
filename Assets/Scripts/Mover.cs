using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    public event Action OnRunning;
    public event Action OnIdle;

    private Rigidbody2D _rigidbody;

    private float _minSpeed = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalDirection, float verticalDirection)
    {
        Vector2 vector = new(horizontalDirection, verticalDirection);
        _rigidbody.MovePosition(_rigidbody.position + (vector * _moveSpeed * Time.deltaTime));

        if (Mathf.Abs(vector.x) > _minSpeed || Mathf.Abs(vector.y) > _minSpeed)
        {
            OnRunning?.Invoke();
        }
        else
        {
            OnIdle?.Invoke();
        }
    }
}