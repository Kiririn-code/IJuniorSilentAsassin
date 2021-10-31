using UnityEngine;

public class PlayerAnimation : StateMachineBehaviour
{
    private Rigidbody _player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null)
            _player = animator.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player != null)
            animator.SetFloat("Speed", _player.velocity.magnitude);
    }
}
