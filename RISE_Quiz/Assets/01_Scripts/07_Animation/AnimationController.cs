using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected string boolName;
    [SerializeField] protected string triggerName;

    public Action OnEventTriggered { get; set; }
    public Action OnEndEventTriggered { get; set; }

    public void ToggleAnim(bool toggle)
    {
        if(boolName == "")
        {
            Debug.LogWarning("Bool parameter is not set!");
            return;
        }

        animator.SetBool(boolName, toggle);
    }

    public void TriggerAnim()
    {
        if (triggerName == "")
        {
            Debug.LogWarning("Trigger parameter is not set!");
            return;
        }

        animator.SetTrigger(triggerName);
    }

    // Animation Event
    protected virtual void TriggerEvent()
    {
        OnEventTriggered?.Invoke();
    }

    protected virtual void TriggerEndEvent()
    {
        OnEndEventTriggered?.Invoke();
    }
}
