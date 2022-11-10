using System;
using UnityEngine;

public class QuestionController : BaseController
{
    [SerializeField] protected AnswerController answerPrefab;
    [SerializeField] protected TextFitter textFitter;
    [SerializeField] protected Transform answerParent;
    [SerializeField] protected float waitTime = 1f;
    [SerializeField] protected float heightCap = 250f;

    [Header("Confirm Button")]
    [SerializeField] protected MenuButtonController confirmButton;

    protected AnswerController[] answers;
    protected MultipleChoiceQuestion Question { get; set; }
    public bool CanBeValidated => Question.CanBeValidated();
    public bool AnswerAnimationEnded { get; set; } = false;

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

        foreach (var answer in answers)
        {
            answer.OnSelect += ActionOnAnswerSelected;
            answer.OnDeselect += ActionOnAnswerDeselected;
        }

        textFitter.FitVerticallyToText(heightCap);

        confirmButton.transform.SetAsLastSibling();
    }

    protected void ActionOnAnswerSelected(AnswerController trigger)
    {
        foreach(var answer in answers)
        {
            if(answer != trigger)
            {
                answer.DeselectWithNotify();
            }
        }

        confirmButton.Toggle(CanBeValidated);
    }

    protected void ActionOnAnswerDeselected(AnswerController trigger)
    {
        confirmButton.Toggle(CanBeValidated);
    }


    public QuestionState Verify()
    {
        confirmButton.Toggle(false);

        AnswerAnimationEnded = false;

        Question.Verify();

        foreach(var answer in answers)
        {
            answer.Verify();
        }

        Invoke(nameof(EndAnimation), waitTime);

        confirmButton.Toggle(true);
        return Question.State;
    }

    public void ResetQuestion()
    {
        Question.ResetQuestion();
        foreach(var answer in answers)
        {
            answer.ResetAnswer();
        }

        confirmButton.Toggle(CanBeValidated);
    }

    private void EndAnimation()
    {
        AnswerAnimationEnded = true;
    }
}
