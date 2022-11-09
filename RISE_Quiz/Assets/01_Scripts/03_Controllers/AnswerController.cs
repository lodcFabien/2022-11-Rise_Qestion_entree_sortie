using UnityEngine;

public class AnswerController : QuizController
{
    [SerializeField] protected TextFitter textFitter;

    public MultipleChoiceAnswer Answer { get; protected set; }

    public override void Populate(QuizItem answer)
    {
        Answer = answer as MultipleChoiceAnswer;
        textField.text = Answer.Text;
        textFitter.FitToText();
    }
}
