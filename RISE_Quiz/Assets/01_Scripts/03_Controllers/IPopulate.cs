using TMPro;
using UnityEngine;

public abstract class QuizController : MonoBehaviour
{
    [SerializeField] protected TMP_Text textField;

    public abstract void Populate(QuizItem quizItem);
}
