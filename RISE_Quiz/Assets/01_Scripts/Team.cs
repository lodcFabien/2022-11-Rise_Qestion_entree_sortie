using UnityEngine;

[System.Serializable]
public class Team
{
    [SerializeField] private string teamNumber;

    // Internal, from 0 to 6
    public int ID { get; private set; }

    // Only for Display, from 1 to 28
    public string Name => GetTeamName();

    private string GetTeamName()
    {
        var language = LocalizationManager.Instance.ActiveLanguage;

        return language == Language.French ? $"ÉQUIPE {teamNumber}" : $"TEAM {teamNumber}";
    }

    public void Init(int id)
    {
        ID = id;
    }
}
