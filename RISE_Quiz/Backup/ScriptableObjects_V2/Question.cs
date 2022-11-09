using UnityEngine;

public abstract class Question
{
    public int ID { get; protected set; }
    public string Text { get; protected set; }
    public QuestionState State { get; protected set; }
    public abstract void Verify();
    public abstract bool CanBeValidated();

    public void ResetQuestion()
    {
        SetState(QuestionState.Unanswered);
    }

    public void SetState(QuestionState newState)
    {
        State = newState;
    }

}
public enum QuestionState
{
    Unanswered,
    AnsweredCorrectly,
    AnsweredWrong
}