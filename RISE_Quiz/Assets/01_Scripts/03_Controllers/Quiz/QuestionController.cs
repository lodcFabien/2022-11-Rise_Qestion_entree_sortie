using UnityEngine;

public class QuestionController : BaseController
{
    [SerializeField] protected AnswerController answerPrefab;
    [SerializeField] protected Transform answerParent;

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
    }

    public void ResetQuestion()
    {
        Question.ResetQuestion();
    }
}
