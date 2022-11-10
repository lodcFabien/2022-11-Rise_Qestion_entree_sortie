using UnityEngine;

public class AnswerButtonView : ButtonView
{
    [SerializeField] private Animator animator;

    public void Select()
    {
        animator.SetBool("isSelected", true);
    }

    public void Deselect()
    {
        animator.SetBool("isSelected", false);
    }
}
