using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quiz/Team")]
public class Team : ScriptableObject
{
    [SerializeField] private string teamNumber;
    [SerializeField] private int teamID;

    // Internal, from 0 to 6
    public int ID => teamID;

    // Only for Display, from 1 to 28
    public string Name => GetTeamName();

    private string GetTeamName()
    {
        var language = LocalizationManager.Instance.ActiveLanguage;

        return language == Language.French ? $"ÉQUIPE {teamNumber}" : $"TEAM {teamNumber}";
    }

    public void Init(string teamNumber, int ID)
    {
        this.teamNumber = teamNumber;
        teamID = ID;
    }
}
