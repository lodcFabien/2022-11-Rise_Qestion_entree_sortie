using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour
{
    [SerializeField] protected TMP_Text textField;
    [SerializeField] protected Transform answerParent;
    [SerializeField] protected List<AnswerController> answerControllers;
    
    public MultipleChoiceQuestion Question { get; protected set; }
    public List<AnswerController> AnswerControllers => answerControllers;
    public Transform AnswerParent => answerParent;

    public void SetQuestionData(MultipleChoiceQuestion question)
    {
        Question = question;
    }

    public void SetQuestionText()
    {
        textField.text = Question.Text;
    }
    public void AddAnswer(AnswerController answerController)
    {
        answerControllers.Add(answerController);
    }
}
