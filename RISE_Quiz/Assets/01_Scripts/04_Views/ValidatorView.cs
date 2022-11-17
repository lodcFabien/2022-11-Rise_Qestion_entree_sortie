using UnityEngine;

public class ValidatorView : BaseView
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Color correctColor;
    [SerializeField] protected Color wrongColor;

    public void SetColor(bool correct)
    {
        if(correct)
        {
            textField.color = correctColor;
        }
        else
        {
            textField.color = wrongColor;
        }
    }

    public void SetVisible(bool visible)
    {
        animator.SetBool("visible", visible);
    }
}
