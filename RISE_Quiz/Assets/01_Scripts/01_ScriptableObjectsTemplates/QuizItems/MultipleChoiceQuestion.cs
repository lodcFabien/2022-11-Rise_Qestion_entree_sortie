using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quiz/Questions/MultipleChoiceQuestion")]
public class MultipleChoiceQuestion : Question
{
    [SerializeField] private MultipleChoiceAnswer[] answers;

    public MultipleChoiceAnswer[] Answers => answers;

    public override void ResetQuestion()
    {
        base.ResetQuestion();
        foreach(var a in answers)
        {
            a.ResetParameters();
        }
    }

    public override bool CanBeValidated()
    {
        foreach (MultipleChoiceAnswer answer in answers)
        {
            if (answer.IsChecked)
            {
                return true;
            }
        }

        return false;
    }

    public override QuestionState Verify()
    {
        foreach (MultipleChoiceAnswer answer in answers)
        {
            if ((answer.IsCorrect() && !answer.IsChecked) || (!answer.IsCorrect() && answer.IsChecked))
            {
                SetState(QuestionState.AnsweredWrong);
                return state;
            }
        }

        SetState(QuestionState.AnsweredCorrectly);
        return state;
    }
}
