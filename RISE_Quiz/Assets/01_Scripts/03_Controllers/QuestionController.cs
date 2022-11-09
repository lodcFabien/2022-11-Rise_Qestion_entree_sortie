using UnityEngine;

public class QuestionController : QuizController
{
    [SerializeField] protected GameObject togglePanel;
    [SerializeField] protected AnswerController answerPrefab;
    [SerializeField] protected Transform answerParent;

    protected AnswerController[] answers;
    public MultipleChoiceQuestion Question { get; protected set; }

    public override void Populate(QuizItem question)
    {
        Question = question as MultipleChoiceQuestion;
        textField.text = Question.Text;

        answers = new AnswerController[Question.Answers.Length];

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i] = Instantiate(answerPrefab, answerParent);
            answers[i].Populate(Question.Answers[i]);
            answers[i].gameObject.name += $" #{i}";
        }
    }

    public void Toggle(bool toggle)
    {
        togglePanel.SetActive(toggle);
    }

    public void ResetQuestion()
    {
        Question.ResetQuestion();
    }
}
