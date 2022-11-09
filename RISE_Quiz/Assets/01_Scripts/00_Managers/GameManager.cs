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
    }

    private void Setup()
    {
        // Get Terminal Config
        terminalConfig = data.GetTerminalFromID(terminalID);

        // Populate Interface
        entryQuestion.Init(terminalConfig.EntryQuestion);
        exitQuestion.Init(terminalConfig.ExitQuestion);
        entryQuestion.Toggle(false);
        exitQuestion.Toggle(false);

        // Set First Group and First Team
        SetGroup(data.GetGroupFromLetter(GroupLetter.A));

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
        entryQuestion.Toggle(state == QuizState.EntryQuestion);
        exitQuestion.Toggle(state == QuizState.ExitQuestion);
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
                SetNextGroup();
                SetState(QuizState.WaitingForStart);
                break;

            case QuizState.EntryQuestion:
            case QuizState.ExitQuestion:
                VerifyAnswer();
                break;

            case QuizState.VerifyingAnswer:
                break;
        }
    }

    private void VerifyAnswer()
    {
        SetState(QuizState.VerifyingAnswer);

        if (!currentQuestion.Question.CanBeValidated())
        {
            Debug.LogWarning("You must select at least one answer before validating!");
            return;
        }

        StartCoroutine(VerifyCoroutine(AnsweredCorrectly(currentQuestion.Question)));
    }

    private IEnumerator VerifyCoroutine(bool answerIsCorrect)
    {
        if(answerIsCorrect)
        {
            view.DoCorrectAnimation();
            yield return new WaitUntil(() => view.CorrectAnimationEnded);

            if (currentQuestion == entryQuestion)
            {
                SetState(QuizState.WaitingForExpertSpeech);
            }
            else
            {
                SetState(QuizState.DisplayingHint);
            }
        }
        else
        {
            view.DoWrongAnimation();
            yield return new WaitUntil(() => view.WrongAnimationEnded);

            if (currentQuestion == exitQuestion)
            {
                SetState(QuizState.EntryQuestion);
            }
            else
            {
                SetState(QuizState.ExitQuestion);
            }
        }
    }

    public void SetNextTeam()
    {
        if (currentTeamOrderIndex == sortedTeams.Count - 1)
        {
            Debug.Log("No more teams!");
            Debug.Log("Game Finished.");
            return;
        }

        currentTeamOrderIndex++;
        SetTeam();
    }

    public void SetNextGroup()
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

    public void SetGroup(GroupConfig newGroupConfig)
    {
        currentGroup = newGroupConfig;
        sortedTeams.Clear();
        sortedTeams = currentGroup.GetSortedTeams(terminalConfig.TeamOrder);
        currentTeamOrderIndex = 0;
        currentQuestion = null;
        SetTeam();

        OnGroupChanged?.Invoke(currentGroup);
    }

    private void SetTeam()
    {
        currentTeam = sortedTeams[currentTeamOrderIndex];
        view.SetTeamName(currentTeam.Name);
    }

    private bool AnsweredCorrectly(MultipleChoiceQuestion question)
    {
        question.Verify();
        return question.State == QuestionState.AnsweredCorrectly;
    }

    #endregion


}
