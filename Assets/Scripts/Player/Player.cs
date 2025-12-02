using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Apple = "Apple";

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private Inventory _inventory;

    private bool _isRunning = false;

    private void Start()
    {
        _mover.OnIdle += OnIdle;
        _mover.OnRunning += OnRunning;
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);
    }

    private void Update()
    {
        _visualizer.TakePositions(GetPosition(), _inputReader.GetMousePosition());
    }

    private void OnDestroy()
    {
        _mover.OnIdle -= OnIdle;
        _mover.OnRunning -= OnRunning;
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
}
