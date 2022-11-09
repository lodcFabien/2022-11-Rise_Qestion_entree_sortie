using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/Terminal")]
public class TerminalConfig : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private MultipleChoiceQuestion entryQuestion;
    [SerializeField] private MultipleChoiceQuestion exitQuestion;
    [SerializeField] private string[] teamHints;

    public int ID => id;
    private int EntryQID => (id * 10) + 1;
    private int ExitQID => EntryQID + 1;
    public MultipleChoiceQuestion EntryQuestion => entryQuestion;
    public MultipleChoiceQuestion ExitQuestion => exitQuestion;
    public int[] TeamOrder => QuizUtils.GetTeamOrderFromTerminalID(id);
    public string[] TeamHints => teamHints;

    public void Init(List<MultipleChoiceQuestion> questions, string[] hints)
    {
        entryQuestion = questions.Find(x => x.ID == EntryQID);
        exitQuestion = questions.Find(x => x.ID == ExitQID);

        teamHints = new string[hints.Length];

        for (int i = 0; i < teamHints.Length; i++)
        {
            teamHints[i] = hints[i][QuizUtils.GetLetterHintIndexByTerminalID(id)].ToString();
        }
    }
}

[System.Serializable]
public struct TeamHint
{
    [SerializeField] private int teamID;
    [SerializeField] private string associatedLetter;

    public int TeamID => teamID;
    public string AssociatedLetter => associatedLetter;
}