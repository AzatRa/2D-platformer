using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _maxRoamingDistance = 7f;
    [SerializeField] private float _minRoamingDintance = 3f;
    [SerializeField] private float _maxRoamingTimer = 2f;
    [SerializeField] private float _maxIdleTimer = 3f;
    [SerializeField] private EnemyVisualizer _visualizer;

    private NavMeshAgent _navMeshAgent;
    private State _state;
    private float _timer;
    private Vector3 _roamingTargetPosition;
    private Vector3 _zeroVector = new Vector3(0, 0, 0);
    private Vector3 _roamingStartingPosition;
    private bool _isRunning = false;

    private enum State
    {
        Idle,
        Roaming
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _state = _startingState;
    }

    private void Start()
    {
        _roamingStartingPosition = transform.position;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        switch (_state)
        {
            default:
            case State.Idle:
                Idle();
                break;
            case State.Roaming:
                Roaming();
                break;
        }
    }

    private void Roaming()
    {
        if (_roamingTargetPosition == _zeroVector)
        {
            _roamingTargetPosition = GetRoamingPosition();
            ChangeFacingDirection(transform.position, _roamingTargetPosition);
            _isRunning = true;
            _visualizer.SwitchAnimation(_isRunning);
            _navMeshAgent.SetDestination(_roamingTargetPosition);
        }

        if (_timer >= _maxRoamingTimer)
        {
            _navMeshAgent.SetDestination(transform.position);
            _state = State.Idle;
            _timer = 0;
            _roamingTargetPosition = _zeroVector;
        }
    }

    private void Idle()
    {
        _isRunning = false;
        _visualizer.SwitchAnimation(_isRunning);

        if (_timer >= _maxIdleTimer)
        {
            _state = State.Roaming;
            _timer = 0;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        float minValue = -1;
        float maxValue = 1;

        Vector3 direction = new Vector3(Random.Range(minValue, maxValue), Random.Range(minValue, maxValue)).normalized;
        Vector3 position = _roamingStartingPosition + direction * Random.Range(_minRoamingDintance, _maxRoamingDistance + 1);
        return position;
    }

    private void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        int angleForRotation = 180;

        if (currentPosition.x < targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, angleForRotation, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
