using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateByQuizState : MonoBehaviour, IToggleable
{
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private List<QuizState> quizStates;

    public void Toggle(bool toggle)
    {
        toggleObject.SetActive(toggle);
    }

    private void Awake()
    {
        Toggle(quizStates.Contains(GameManager.Instance.State));
        GameManager.Instance.OnQuizStateChanged += UpdateState;
    }

    private void UpdateState(QuizState state)
    {
        Toggle(quizStates.Contains(state));
    }
}
