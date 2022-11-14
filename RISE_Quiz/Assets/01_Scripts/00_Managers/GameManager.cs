using System;
using System.Collections;
using System.Collections.Generic;
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

    // MANUAL
    [Header("Manually Set")]
    [Header("Data")]
    [SerializeField] private Data data;

    [Header("UI")]
    [SerializeField] private GameView view;

    [Header("Question Controllers")]
    [SerializeField] private QuestionController entryQuestion;
    [SerializeField] private QuestionController exitQuestion;

    [Header("Panel Controllers")]
    [SerializeField] private PanelController setupPanel;
    [SerializeField] private WaitPanelController waitForStartPanel;
    [SerializeField] private WaitPanelController expertSpeechPanel;
    [SerializeField] private HintPanelController hintPanel;

    // DYNAMIC
    [Header("Dynamically Set")]
    [Header("State")]
    [SerializeField] private QuizState state = QuizState.Init;

    [Header("Terminal")]
    [SerializeField] private int terminalID = -1;
    [SerializeField] private TerminalConfig terminalConfig;

    [Header("Team Order & ID")]
    [SerializeField] private int currentTeamOrderIndex = -1;
    [SerializeField] private Team currentTeam;
    [SerializeField] private List<Team> sortedTeams;

    [Header("Current Question")]
    [SerializeField] private QuestionController currentQuestion;

    public Action<QuizState> OnQuizStateChanged { get; set; }
    public Action<int> OnTeamChanged { get; set; }
    public QuizState State => state;
    public Team[] Teams => data.Teams;
    
    private bool EntryQuestionDisplayed => state == QuizState.EntryQuestion ||
        state == QuizState.Verifying && currentQuestion == entryQuestion;

    private bool ExitQuestionDisplayed => state == QuizState.ExitQuestion ||
        state == QuizState.Verifying && currentQuestion == exitQuestion;

    #region Unity Messages

    private void Awake()
    {
        InitializeSingleton();
        data.Init();
    }

    private void Start()
    {
        view.Init();
        SetState(QuizState.Setup);
    }

    #endregion

    #region Setup

    public void SetTerminalID(int id)
    {
        terminalID = id;
        terminalConfig = data.GetTerminalFromID(terminalID);
        SortTeams();

        currentTeamOrderIndex = 0;
        SetTeam();
    }

    private void Setup()
    {
        // Populate Interface
        entryQuestion.Setup(terminalConfig.EntryQuestion);
        exitQuestion.Setup(terminalConfig.ExitQuestion);
        entryQuestion.Toggle(false);
        exitQuestion.Toggle(false);
        currentQuestion = null;

        SetState(QuizState.WaitingForStart);
    }

    #endregion

    #region State

    public void SetState(QuizState newState)
    {
        if (state == newState)
        {
            return;
        }

        state = newState;

        OnQuizStateChanged?.Invoke(state);

        setupPanel.Toggle(state == QuizState.Setup);
        waitForStartPanel.Toggle(state == QuizState.WaitingForStart);
        expertSpeechPanel.Toggle(state == QuizState.WaitingForExpertSpeech);
        hintPanel.Toggle(state == QuizState.DisplayingHint);
        entryQuestion.Toggle(EntryQuestionDisplayed);
        exitQuestion.Toggle(ExitQuestionDisplayed);

        switch (state)
        {
            case QuizState.EntryQuestion:
                entryQuestion.ResetQuestion();
                currentQuestion = entryQuestion;
                break;
            case QuizState.ExitQuestion:
                exitQuestion.ResetQuestion();
                currentQuestion = exitQuestion;
                break;
            case QuizState.DisplayingHint:
                hintPanel.SetHint(terminalConfig.GetHint(currentTeam.ID), LocalizationManager.Instance.ActiveLanguage, GetHintNumber());
                break;
        }
    }

    #endregion

    #region Button Methods

    public void OnConfirmButton()
    {
        switch (state)
        {
            case QuizState.Setup:
                Setup();
                break;

            case QuizState.WaitingForStart:
                SetState(QuizState.EntryQuestion);
                break;

            case QuizState.WaitingForExpertSpeech:
                SetState(QuizState.ExitQuestion);
                break;

            case QuizState.DisplayingHint:
                hintPanel.HideHint();
                if (NoMoreTeams())
                {
                    Debug.Log("No more teams!");
                    ResetEverything();
                }
                else
                {
                    SetNextTeam();
                }
                break;

            case QuizState.EntryQuestion:
            case QuizState.ExitQuestion:
                VerifyQuestion();
                break;

            case QuizState.Verifying:
                break;
        }
    }

    #endregion

    #region Public Methods

    public void SetTeamByDropdown(int index)
    {
        currentTeam = data.Teams[index];
        currentTeamOrderIndex = sortedTeams.IndexOf(currentTeam);
        view.SetTeamName(currentTeam.Name);
    }

    public void ResetEverything()
    {
        entryQuestion.ResetQuestion(true);
        exitQuestion.ResetQuestion(true);
        SetState(QuizState.Setup);
    }

    #endregion

    #region Utils

    private void SortTeams()
    {
        sortedTeams.Clear();
        sortedTeams = data.GetSortedTeams(terminalID);
    }

    private void SetTeam()
    {
        currentTeam = sortedTeams[currentTeamOrderIndex];
        view.SetTeamName(currentTeam.Name);
        OnTeamChanged?.Invoke(Array.IndexOf(Teams, currentTeam));
        //Debug.Log($"Team ID #{currentTeam.ID}, Name{currentTeam.Name}, will obtain its hint number {currentTeamOrderIndex}");
    }

    private void VerifyQuestion()
    {
        SetState(QuizState.Verifying);

        if (!currentQuestion.CanBeValidated)
        {
            Debug.LogWarning("You must select at least one answer before validating!");
            return;
        }

        currentQuestion.Verify(currentQuestion == entryQuestion ? 0 : 1);
    }

    private void SetNextTeam()
    {
        currentTeamOrderIndex++;
        SetTeam();
        SetState(QuizState.WaitingForStart);
    }

    private bool NoMoreTeams()
    {
        return currentTeamOrderIndex == sortedTeams.Count - 1;
    }

    private int GetHintNumber()
    {
        return currentTeamOrderIndex+1;
    }

    #endregion

}
