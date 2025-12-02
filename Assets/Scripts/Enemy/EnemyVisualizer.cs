using UnityEngine;

public class EnemyVisualizer : MonoBehaviour
{
    private const string IsRunning = "IsRunning";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }
}
