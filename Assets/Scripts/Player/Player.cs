using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Apple = "Apple";

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    //[SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private Inventory _inventory;

    private bool _isRunning = false;
    //private bool _isJumpButtonHold = false;
    //private float _jumpHoldTime = 0f;
    //private float _maxJumpHoldTime = 1f;

    private void Start()
    {
        _mover.OnIdle += OnIdle;
        _mover.OnRunning += OnRunning;
        //_inputReader.OnJumpHoldStart += OnJumpHoldStart;
        //_inputReader.OnJumpHoldEnd += OnJumpHoldEnd;
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);

        //if (_isJumpButtonHold)
        //{
        //    _jumpHoldTime += Time.deltaTime;

        //    if (_jumpHoldTime > _maxJumpHoldTime)
        //        _jumpHoldTime = _maxJumpHoldTime;
        //}
    }

    private void Update()
    {
        _visualizer.TakePositions(GetPosition(), _inputReader.GetMousePosition());
    }

    private void OnDestroy()
    {
        _mover.OnIdle -= OnIdle;
        _mover.OnRunning -= OnRunning;
        //_inputReader.OnJumpHoldStart -= OnJumpHoldStart;
        //_inputReader.OnJumpHoldEnd -= OnJumpHoldEnd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Apple)
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                _inventory.Take(coin);
                Destroy(collision.gameObject);
            }
        }
    }

    private Vector2 GetPosition()
    {
        Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
        return position;
    }

    private void OnRunning()
    {
        _isRunning = true;
        SwitchAnimation(_isRunning);
    }

    private void OnIdle()
    {
        _isRunning = false;
        SwitchAnimation(_isRunning);
    }

    private void SwitchAnimation(bool isRunning)
    {
        _visualizer.SwitchAnimation(isRunning);
    }

    //private void OnJumpHoldStart()
    //{
    //    _isJumpButtonHold = true;
    //    _jumpHoldTime = 0f;
    //}

    //private void OnJumpHoldEnd()
    //{
    //    _isJumpButtonHold = false;

    //    if (_groundDetector.IsGround)
    //    {
    //        _mover.Jump(_jumpHoldTime / _maxJumpHoldTime);
    //        _visualizer.Jump();
    //    }
    //}
}
