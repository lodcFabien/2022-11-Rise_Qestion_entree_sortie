using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quiz/Questions/MultipleChoiceQuestion")]
public class MultipleChoiceQuestion : Question
{
    [SerializeField] private MultipleChoiceAnswer[] answers;

    public MultipleChoiceAnswer[] Answers => answers;

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

    public override void Verify()
    {
        foreach (MultipleChoiceAnswer answer in answers)
        {
            if ((answer.IsCorrect() && !answer.IsChecked) || (!answer.IsCorrect() && answer.IsChecked))
            {
                SetState(QuestionState.AnsweredWrong);
                return;
            }
        }

        SetState(QuestionState.AnsweredCorrectly);
    }
}
