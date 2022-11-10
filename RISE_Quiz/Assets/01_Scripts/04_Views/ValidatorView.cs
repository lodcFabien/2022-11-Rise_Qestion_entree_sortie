using UnityEngine;

public class ValidatorView : BaseView
{
    [SerializeField] protected Animator animator;

    public void SetVisible(bool visible)
    {
        animator.SetBool("visible", visible);
    }
}
