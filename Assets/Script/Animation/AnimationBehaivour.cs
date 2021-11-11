using UnityEngine.AI;
using UnityEngine;

public class AnimationBehaivour : StateMachineBehaviour
{
    private EnemyController _enemy;
    private Animator _animator;

    private const string _speed = "Speed";
    private const string _idle = "Idle";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_enemy == null)
            _enemy = animator.gameObject.GetComponent<EnemyController>();
        _animator = animator;
        _enemy.BehaivourChanged += OnIdleEnter;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy.BehaivourChanged -= OnIdleEnter;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemy != null)
            animator.SetFloat(_speed, _enemy.GetMagnitude());
    }

    private void OnIdleEnter()
    {
        _animator.SetTrigger(_idle);
    }
}

