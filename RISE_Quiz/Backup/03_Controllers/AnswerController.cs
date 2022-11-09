using UnityEngine;
using TMPro;

public class AnswerController : MonoBehaviour
{
    [SerializeField] protected TMP_Text textField;

    public Answer Answer { get; protected set; }

    public void Init(Answer answer)
    {
        Answer = answer;
    }

    protected void SetAnswerText()
    {
        textField.text = Answer.Text;
    }
}
