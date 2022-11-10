using UnityEngine;

public class AnswerView : ButtonView
{
    public void SetAnimatorState(int stateIndex)
    {
        animator.SetInteger("state", stateIndex);
    }
}
