using UnityEngine;

public class MultipleChoiceQuestion : Question
{
    public MultipleChoiceQuestion(int id, string text, MultipleChoiceAnswer[] answers)
    {
        ID = id;
        Text = text;
        Answers = answers;
        State = QuestionState.Unanswered;
    }

    public MultipleChoiceAnswer[] Answers { get; protected set; }

    public override bool CanBeValidated()
    {
        foreach (MultipleChoiceAnswer answer in Answers)
        {
            if (answer.IsChecked)
            {
                return true;
            }
        }

        return false;
    }

    public override void Verify()
    {
        foreach (MultipleChoiceAnswer answer in Answers)
        {
            if ((answer.IsCorrect && !answer.IsChecked) || (!answer.IsCorrect && answer.IsChecked))
            {
                SetState(QuestionState.AnsweredWrong);
                return;
            }
        }

        SetState(QuestionState.AnsweredCorrectly);
    }
}
