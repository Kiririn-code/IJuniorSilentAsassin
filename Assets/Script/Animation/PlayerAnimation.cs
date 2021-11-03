using UnityEngine;

public class PlayerAnimation : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            if (Input.GetKey(KeyCode.W))
                animator.SetBool("IsWalk", true);
            else
                animator.SetBool("IsWalk", false);
    }
}
