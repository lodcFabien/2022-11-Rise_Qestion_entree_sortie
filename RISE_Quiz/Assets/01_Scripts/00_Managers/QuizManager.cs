using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private QuizState state = QuizState.NotStarted;

    [Header("Team Order & ID")]
    [SerializeField] private Group currentGroup;
    [SerializeField] private int[] teamOrder;
    [SerializeField] private int currentTeamID;
    [SerializeField] private int currentTeamOrderIndex;

    [Header("Questions")]
    [SerializeField] private List<MultipleChoiceQuestion> questions;

    [Header("Question Controllers")]
    [SerializeField] private QuestionController entryQuestion;
    [SerializeField] private QuestionController exitQuestion;

    [Header("UI")]
    [SerializeField] private TMP_Text teamIDText;

    private int TerminalID { get; set; }
    public Action<QuizState> OnQuizStateChanged { get; set; }

    public void Init(int terminalID)
    {
        // Set Terminal ID
        TerminalID = terminalID;

        // Get Questions from Terminal ID
        var ids = QuizUtils.GetQuestionIDsFromTerminalID(TerminalID);
        var entryQ = GetQuestionByID(ids[0]);
        var exitQ = GetQuestionByID(ids[1]);

        // Populate Interface
        entryQuestion.Populate(entryQ);
        exitQuestion.Populate(exitQ);
        entryQuestion.Toggle(false);
        exitQuestion.Toggle(false);

        // Set First Group and First Team
        SetGroup(Group.A);
        teamOrder = QuizUtils.GetTeamOrderFromTerminalID(TerminalID);
        currentTeamOrderIndex = 0;
        SetTeam(currentTeamOrderIndex);
    }

    #region Public Methods

    public void StartQuiz()
    {
        SetState(QuizState.EntryQuestion);
    }

    public void Validate()
    {

    }

    public void SetNextTeam()
    {
        if(currentTeamOrderIndex == teamOrder.Length -1)
        {
            Debug.Log("No more teams!");
            Debug.Log("Game Finished.");
            return;
        }
        
        currentTeamOrderIndex++;
        SetTeam(currentTeamOrderIndex);
    }

    public void SetGroup(Group newGroup)
    {
        currentGroup = newGroup;
    }

    #endregion

    #region Utils

    private MultipleChoiceQuestion GetQuestionByID(int id)
    {
        return questions.Find(x => x.ID == id);
    }

    private void SetTeam(int teamOrderIndex)
    {
        currentTeamID = teamOrder[teamOrderIndex];
        SetTeamName();
    }

    private void SetTeamName()
    {
        int teamNumber = currentTeamID;
        switch (currentGroup)
        {
            case Group.B:
                teamNumber += 7;
                break;
            case Group.C:
                teamNumber += 14;
                break;
            case Group.D:
                teamNumber += 21;
                break;
        }

        teamIDText.text = LocalizationManager.Instance.ActiveLanguage == Language.French ? $"ÉQUIPE {teamNumber}" : $"TEAM {teamNumber}";
    }

    #endregion

    public void SetState(QuizState newState)
    {
        if(state == newState)
        {
            return;
        }

        state = newState;

        OnQuizStateChanged?.Invoke(state);
    }

}
