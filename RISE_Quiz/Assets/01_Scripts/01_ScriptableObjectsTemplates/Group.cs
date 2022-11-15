using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quiz/Group")]
public class Group : ScriptableObject
{
    [SerializeField] private GroupLetter letter;
    [SerializeField] private List<Team> teams;

    public GroupLetter Letter => letter;
    public List<Team> Teams => teams;

    public void Init(GroupLetter letter, List<Team> teams)
    {
        this.letter = letter;
        this.teams = teams;
    }

    public void SetTeams(List<Team> teams)
    {
        this.teams = teams;
    }

    public string GetGroupName(Language language)
    {
        return language == Language.French ? $"GROUPE {letter}" : $"GROUP {letter}";
    }
}
