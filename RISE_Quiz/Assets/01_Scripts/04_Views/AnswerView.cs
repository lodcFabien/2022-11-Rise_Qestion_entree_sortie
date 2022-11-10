using UnityEngine;

public class AnswerView : ButtonView
{
    public void SetAnimatorState(int stateIndex)
    {
        if (animator.isActiveAndEnabled)
        {
            animator.SetInteger("state", stateIndex);
        }
    }
}
