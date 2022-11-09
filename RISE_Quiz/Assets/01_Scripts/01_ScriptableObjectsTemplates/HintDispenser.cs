using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quiz/HintDispenser")]
public class HintDispenser : ScriptableObject
{
    [SerializeField] private List<Hint> hints;

    public string GetHint(int teamID)
    {
        return hints.Find(x => x.TeamID == teamID).LetterHint;
    }
}

[System.Serializable]
public struct Hint
{
    [SerializeField][Range(1,7)] private int teamID;
    [SerializeField] private string letterHint;

    public int TeamID => teamID;
    public string LetterHint => letterHint;
}
