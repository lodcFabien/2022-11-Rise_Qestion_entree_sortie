using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void InitializeSingleton()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private int terminalID = -1;
    [SerializeField] private QuizManager quizManager;
     
    [Header("Questions")]
    [SerializeField] private QuestionController entryQuestion;
    [SerializeField] private QuestionController exitQuestion;

    [Header("Prefabs")]
    [SerializeField] private AnswerController acPrefab;

    #region Unity Messages

    private void Awake()
    {
        InitializeSingleton();
    }

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    public void SetTerminalID(int id)
    {
        terminalID = id;
    }

    public void Setup()
    {
        quizManager.Init(terminalID);
    }


    #endregion
}
