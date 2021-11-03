using UnityEngine.AI;
using UnityEngine;

public class AnimationBehaivour : StateMachineBehaviour
{
    private EnemyController _enemy;
    private Animator _animator;

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
            animator.SetFloat("Speed", _enemy.GetMagnitude());
    }

    private void OnIdleEnter()
    {
        _animator.SetTrigger("Idle");
    }
}

