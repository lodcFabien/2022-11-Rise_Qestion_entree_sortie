using UnityEngine;

public class QuestionController : BaseController
{
    [SerializeField] protected AnswerController answerPrefab;
    [SerializeField] protected Transform confirmButton;
    [SerializeField] protected TextFitter textFitter;
    [SerializeField] protected Transform answerParent;
    [SerializeField] protected float heightCap = 250f;

    protected AnswerController[] answers;
    public MultipleChoiceQuestion Question { get; protected set; }

    public void Init(MultipleChoiceQuestion question)
    {
        Question = question;
        view.Populate(Question.Text);

        answers = new AnswerController[Question.Answers.Length];

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i] = Instantiate(answerPrefab, answerParent);
            answers[i].Init(Question.Answers[i]);
            answers[i].gameObject.name += $" #{i}";
        }

        confirmButton.SetAsLastSibling();

        foreach(var answer in answers)
        {
            answer.OnSelect += ActionOnAnswerSelected;
        }

        textFitter.FitVerticallyToText(heightCap);
    }

    protected void ActionOnAnswerSelected(AnswerController trigger)
    {
        foreach(var answer in answers)
        {
            if(answer != trigger)
            {
                answer.Deselect();
            }
        }
    }

    public void ResetQuestion()
    {
        Question.ResetQuestion();
    }
}
