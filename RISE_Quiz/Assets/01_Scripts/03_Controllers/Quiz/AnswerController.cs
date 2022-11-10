using System;
using UnityEngine;

public class AnswerController : ButtonController, ISelectable
{
    [SerializeField] protected AnswerControllerState state;
    [SerializeField] protected TextFitter textFitter;
    [SerializeField] protected float widthCap = 450f;

    protected AnswerView View => view as AnswerView;
    protected MultipleChoiceAnswer Answer { get; set; }
    public Action<AnswerController> OnSelect { get ; set; }
    public Action<AnswerController> OnDeselect { get ; set; }
    public bool IsSelected { get; protected set; }
    public AnswerControllerState State => state;

    public void Init(MultipleChoiceAnswer answer)
    {
        Answer = answer;
        view.Populate(Answer.Text);
        textFitter.FitHorizontallyToText(widthCap);
        ResetAnswer();
    }

    public void SelectWithNotify()
    {
        if (IsSelected) { return; }
        Select();
        OnSelect?.Invoke(this);
    }

    public void SelectWithoutNotify()
    {
        Select();
    }

    public void DeselectWithNotify()
    {
        if (!IsSelected) { return; }
        Deselect();
        OnDeselect?.Invoke(this);
    }

    public void DeselectWithoutNotify()
    {
        Deselect();
    }

    public void OnClick()
    {
        if (IsSelected) DeselectWithNotify();
        else SelectWithNotify();
    }

    public void Verify()
    {
        if (Answer.IsCorrect())
        {
            SetState(AnswerControllerState.Correct);
        }
        else
        {
            SetState(AnswerControllerState.Incorrect);
        }
    }

    public void ResetAnswer()
    {
        Answer.ResetParameters();
        DeselectWithoutNotify();
    }

    private void Select()
    {
        IsSelected = true;
        Answer.Toggle(IsSelected);
        SetState(AnswerControllerState.Selected);
    }

    private void Deselect()
    {
        IsSelected = false;
        Answer.Toggle(IsSelected);
        SetState(AnswerControllerState.Unselected);
    }

    public void SetState(AnswerControllerState newState)
    {
        state = newState;
        View.SetAnimatorState((int)state);
    }
}
public enum AnswerControllerState
{
    Unselected,
    Selected,
    Hidden,
    Correct,
    Incorrect
}
