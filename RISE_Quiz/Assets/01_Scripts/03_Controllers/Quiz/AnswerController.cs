using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerController : ButtonController, ISelectable
{
    [SerializeField] protected TextFitter textFitter;
    [SerializeField] protected float widthCap = 450f;

    public AnswerButtonView View => view as AnswerButtonView;
    public MultipleChoiceAnswer Answer { get; protected set; }
    public Action<AnswerController> OnSelect { get ; set; }
    public Action<AnswerController> OnDeselect { get ; set; }
    public bool IsSelected { get; protected set; }

    public void Init(MultipleChoiceAnswer answer)
    {
        Answer = answer;
        view.Populate(Answer.Text);
        textFitter.FitHorizontallyToText(widthCap);
        SetDeselectedWithoutNotify();
    }

    public void Select()
    {
        if(IsSelected) { return; }

        IsSelected = true;
        View.Select();
        OnSelect?.Invoke(this);

        Answer.Toggle(IsSelected);
    }

    public void Deselect()
    {
        if(!IsSelected) { return; }

        IsSelected = false;
        View.Deselect();
        OnDeselect?.Invoke(this);

        Answer.Toggle(IsSelected);
    }

    public void SetSelectedWithoutNotify()
    {
        IsSelected = true;
        View.Select();
        Answer.Toggle(IsSelected);
    }

    public void SetDeselectedWithoutNotify()
    {
        IsSelected = false;
        View.Deselect();
        Answer.Toggle(IsSelected);
    }

    public void OnClick()
    {
        if (IsSelected) Deselect();
        else Select();
    }
}
public enum AnswerState
{

}
