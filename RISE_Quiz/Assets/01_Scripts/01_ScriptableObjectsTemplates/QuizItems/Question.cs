using UnityEngine;

public abstract class Question : QuizItem
{
    [SerializeField] protected int id;
    [SerializeField] protected bool shouldBeCentered = false;

    [Header("Debug")]
    [SerializeField] protected QuestionState state = QuestionState.Unanswered;
    public int ID => id;
    public bool ShouldBeCentered => shouldBeCentered;
    public QuestionState State => state;
    public abstract QuestionState Verify();
    public abstract bool CanBeValidated();

    public virtual void ResetQuestion()
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