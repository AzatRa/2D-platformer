using UnityEngine;

public class PlayerVisualizer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private const string IsRunning = "IsRunning";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _playerPosition;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AdjustPlayerFacingDirection();
    }

    public void SwitchAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }

    //public void Jump()
    //{
    //    _animator.SetTrigger(IsJump);
    //}

    public void TakePositions(Vector2 playerPosition, Vector2 mousePosition)
    {
        _playerPosition = playerPosition;
        _mousePosition = mousePosition;
    }

    private void AdjustPlayerFacingDirection()
    {
        if (_mousePosition.x < _playerPosition.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

}


