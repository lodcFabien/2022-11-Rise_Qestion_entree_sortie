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
    [SerializeField] private PanelController waitForStartPanel;
    [SerializeField] private PanelController expertSpeechPanel;
    [SerializeField] private PanelController hintPanel;
    [SerializeField] private PanelController endPanel;

    // DYNAMIC
    [Header("Dynamically Set")]
    [Header("Terminal")]
    [SerializeField] private int terminalID = -1;
    [SerializeField] private TerminalConfig terminalConfig;

    [Header("State")]
    [SerializeField] private QuizState state = QuizState.Init;

    [Header("Team Order & ID")]
    [SerializeField] private GroupConfig currentGroup;
    [SerializeField] private Team currentTeam;
    [SerializeField] private List<Team> sortedTeams;
    [SerializeField] private int currentTeamOrderIndex = -1;

    [Header("Current Question")]
    [SerializeField] private QuestionController currentQuestion;

    public Action<QuizState> OnQuizStateChanged { get; set; }
    public Action<GroupConfig> OnGroupChanged { get; set; }
    public QuizState State => state;
    
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
    }

    private void Setup()
    {
        // Populate Interface
        entryQuestion.Init(terminalConfig.EntryQuestion);
        exitQuestion.Init(terminalConfig.ExitQuestion);
        entryQuestion.Toggle(false);
        exitQuestion.Toggle(false);

        // Set First Group and First Team
        SetGroup(currentGroup);

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
        endPanel.Toggle(state == QuizState.Ended);

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
                break;
            case QuizState.Ended:
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
                SetState(QuizState.Ended);
                break;

            case QuizState.Ended:
                SetNextTeam();
                SetState(QuizState.WaitingForStart);
                break;

            case QuizState.EntryQuestion:
            case QuizState.ExitQuestion:
                VerifyQuestion();
                break;

            case QuizState.Verifying:
                break;
        }
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
        if (currentTeamOrderIndex == sortedTeams.Count - 1)
        {
            Debug.Log("No more teams!");
            SetNextGroup();
        }
        else
        {
            currentTeamOrderIndex++;
            SetTeam();
        }

        SetState(QuizState.Setup);
    }

    private void SetNextGroup()
    {
        switch (currentGroup.GroupLetter)
        {
            case GroupLetter.A:
                SetGroup(data.GetGroupFromLetter(GroupLetter.B));
                break;
            case GroupLetter.B:
                SetGroup(data.GetGroupFromLetter(GroupLetter.C));
                break;
            case GroupLetter.C:
                SetGroup(data.GetGroupFromLetter(GroupLetter.D));
                break;
            case GroupLetter.D:
                SetGroup(data.GetGroupFromLetter(GroupLetter.A));
                break;
        }
    }

    #endregion

    #region Utils

    private void SetGroup(GroupConfig newGroupConfig)
    {
        currentGroup = newGroupConfig;

        sortedTeams.Clear();
        sortedTeams = currentGroup.GetSortedTeams(terminalConfig.TeamOrder);
        currentTeamOrderIndex = 0;
        currentQuestion = null;
        SetTeam();

        OnGroupChanged?.Invoke(currentGroup);
    }

    public void SetGroup(GroupLetter letter)
    {
        SetGroup(data.GetGroupFromLetter(letter));
    }

    public GroupConfig GetCurrentGroup() { return currentGroup; }

    public void SetTeam(int index)
    {
        currentTeamOrderIndex = index;
        SetTeam();
    }

    private void SetTeam()
    {
        currentTeam = sortedTeams[currentTeamOrderIndex];
        view.SetTeamName(currentTeam.Name);
    }

    #endregion

}
