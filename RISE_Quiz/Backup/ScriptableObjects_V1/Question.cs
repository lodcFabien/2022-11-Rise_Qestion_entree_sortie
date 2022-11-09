using UnityEngine;

public abstract class Question : ScriptableObject
{
    [SerializeField] protected int id;
    [SerializeField][TextArea(3, 6)] protected string questionText;

    [Header("Debug")]
    [SerializeField] protected QuestionState state = QuestionState.Unanswered;

    public string Text => questionText;
    public QuestionState State => state;
    public abstract void Verify();
    public abstract bool CanBeValidated();

    public void ResetQuestion()
    {
        SetState(QuestionState.Unanswered);
    }

    public void SetState(QuestionState newState)
    {
        state = newState;
    }

}
public enum QuestionState
{
    Unanswered,
    AnsweredCorrectly,
    AnsweredWrong
}