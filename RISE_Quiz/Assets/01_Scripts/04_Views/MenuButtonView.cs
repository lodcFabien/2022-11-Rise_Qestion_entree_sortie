using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonView : ButtonView
{
    public Action OnClickAnimationEnded { get; set; }
    public void OnClick()
    {
        animator.SetTrigger("onClick");
    }

    // Animation Event
    protected void EventOnClickAnimationEnded()
    {
        OnClickAnimationEnded?.Invoke();
    }

    //public void ToggleButton(bool toggle)
    //{
    //    animator.SetBool("visible", toggle);
    //}
}
