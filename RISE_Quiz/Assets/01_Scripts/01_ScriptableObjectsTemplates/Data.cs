using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/Data")]
public class Data : ScriptableObject
{
    [SerializeField] private List<TerminalConfig> terminals;
    [SerializeField] private List<MultipleChoiceQuestion> questions;
    [SerializeField] private string[] wordsToFind;
    [SerializeField] private Team[] teams;

    private List<Team> groupA;
    private List<Team> groupB;
    private List<Team> groupC;
    private List<Team> groupD;

    public Team[] Teams => teams;

    //private void OnValidate()
    //{
    //    if(doOnValidate)
    //    {
    //        for(int i = 0; i < 28; i++)
    //        {
    //            var teamNumber = i + 1;
    //            var test = CreateInstance<Team>();
    //            test.Init(teamNumber.ToString(), QuizUtils.GetIDByTeamNumber(teamNumber));
    //            AssetDatabase.CreateAsset(test, $"Assets/Team#{teamNumber}.asset");
    //        }

    //        doOnValidate = false;
    //    }
    //}

    public void Init()
    {
        terminals.ForEach(x => x.Init(questions, wordsToFind));
        SetGroups();
    }

    public TerminalConfig GetTerminalFromID(int id)
    {
        return terminals.Find(x => x.ID == id);
    }

    public List<Team> GetSortedTeams(int terminalID)
    {
        var order = QuizUtils.GetTeamOrderFromTerminalID(terminalID);
        var sortedList = new List<Team>();

        var listA = order.Select(i => groupA[i]).ToList();
        var listB = order.Select(i => groupB[i]).ToList();
        var listC = order.Select(i => groupC[i]).ToList();
        var listD = order.Select(i => groupD[i]).ToList();

        sortedList = listA;
        sortedList.AddRange(listB);
        sortedList.AddRange(listC);
        sortedList.AddRange(listD);

        return sortedList;
    }

    private void SetGroups()
    {
        groupA = new List<Team>();
        groupB = new List<Team>();
        groupC = new List<Team>();
        groupD = new List<Team>();

        for (int i = 0; i < teams.Length; i++)
        {
            if (i < 7)
            {
                groupA.Add(teams[i]);
            }
            else if (i < 14)
            {
                groupB.Add(teams[i]);
            }
            else if (i < 21)
            {
                groupC.Add(teams[i]);
            }
            else
            {
                groupD.Add(teams[i]);
            }
        }
    }
}
