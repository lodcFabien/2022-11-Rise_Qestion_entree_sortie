using UnityEngine;

public class AnswerView : ButtonView
{
    [SerializeField] private Animator animator;

    public void SetAnimatorState(int stateIndex)
    {
        animator.SetInteger("state", stateIndex);
    }
}
