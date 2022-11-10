using System;
using System.Collections;
using UnityEngine;

public class QuestionController : BaseController
{
    [SerializeField] protected AnswerController answerPrefab;
    [SerializeField] protected TextFitter textFitter;
    [SerializeField] protected Transform answerParent;
    [SerializeField] protected float heightCap = 250f;

    [Header("Confirm Button")]
    [SerializeField] protected MenuButtonController confirmButton;

    [Header("Validator")]
    [SerializeField] protected ValidatorController validator;

    [Header("Time")]
    [SerializeField] protected float suspenseTime = 1.5f;
    [SerializeField] protected float animTime = 3f;

    protected AnswerController[] answers;
    protected MultipleChoiceQuestion Question { get; set; }
    public bool CanBeValidated => Question.CanBeValidated();
    protected GameManager GameManager => GameManager.Instance;

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

    public void Verify(int currentQuestionIndex)
    {
        StartCoroutine(VerifyCoroutine(currentQuestionIndex));
    }

    private IEnumerator VerifyCoroutine(int currentQuestionIndex)
    {
        confirmButton.Toggle(false);
        Question.Verify();
        bool isCorrect = Question.State == QuestionState.AnsweredCorrectly;
        Debug.Log($"VerifyCoroutine().isCorrect? {isCorrect}");
        HideUnselectedAnswers();

        yield return new WaitForSeconds(suspenseTime);
        VerifySelectedAnswer();

        validator.SetText(isCorrect, LocalizationManager.Instance.ActiveLanguage);
        validator.Toggle(true);

        yield return new WaitForSeconds(animTime);

        if(isCorrect)
        {
            if(currentQuestionIndex == 0)
            {
                GameManager.SetState(QuizState.WaitingForExpertSpeech);
            }
            else
            {
                GameManager.SetState(QuizState.DisplayingHint);
            }
        }
        else
        {
            if(currentQuestionIndex == 0)
            {
                GameManager.SetState(QuizState.EntryQuestion);
            }
            else
            {
                GameManager.SetState(QuizState.ExitQuestion);
            }
        }

        //confirmButton.Toggle(true);
    }

    private void HideUnselectedAnswers()
    {
        foreach (var answer in answers)
        {
            if (answer.State == AnswerControllerState.Unselected)
            {
                answer.SetState(AnswerControllerState.Hidden);
            }
        }
    }

    private void VerifySelectedAnswer()
    {
        foreach(var answer in answers)
        {
            if(answer.State == AnswerControllerState.Selected)
            {
                answer.Verify();
            }
        }
    }

    public void ResetQuestion()
    {
        Question.ResetQuestion();
        foreach(var answer in answers)
        {
            answer.ResetAnswer();
        }

        confirmButton.Toggle(CanBeValidated);
        validator.Toggle(false);
    }
}
