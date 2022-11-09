using UnityEngine;

public abstract class Answer : QuizItem
{
    public abstract bool IsCorrect();
    public abstract void ResetParameters();
    //public abstract bool CanBeValidated();
}
