using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerController : ButtonController
{
    [SerializeField] protected TextFitter textFitter;

    public AnswerButtonView View => view as AnswerButtonView;

    public MultipleChoiceAnswer Answer { get; protected set; }

    public void Init(MultipleChoiceAnswer answer)
    {
        Answer = answer;
        view.Populate(Answer.Text);
        textFitter.FitToText();
    }
}
