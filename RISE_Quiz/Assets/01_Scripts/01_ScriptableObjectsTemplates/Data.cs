using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/Data")]
public class Data : ScriptableObject
{
    [SerializeField] private List<TerminalConfig> terminals;
    [SerializeField] private List<MultipleChoiceQuestion> questions;
    [SerializeField] private string[] wordsToFind;

    [Header("Teams")]
    [SerializeField] private Team[] teams;

    [Header("Groups")]
    [SerializeField] private Group groupA;
    [SerializeField] private Group groupB;
    [SerializeField] private Group groupC;
    [SerializeField] private Group groupD;

    private List<Team> groupAList;
    private List<Team> groupBList;
    private List<Team> groupCList;
    private List<Team> groupDList;

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
    private void OnDestroy()
    {
        ResetGroups();
    }

    public TerminalConfig GetTerminalFromID(int id)
    {
        return terminals.Find(x => x.ID == id);
    }

    public List<Team> GetSortedTeams(int terminalID)
    {
        var order = QuizUtils.GetTeamOrderFromTerminalID(terminalID);
        var sortedList = new List<Team>();

        var listA = order.Select(i => groupAList[i]).ToList();
        var listB = order.Select(i => groupBList[i]).ToList();
        var listC = order.Select(i => groupCList[i]).ToList();
        var listD = order.Select(i => groupDList[i]).ToList();

        sortedList = listA;
        sortedList.AddRange(listB);
        sortedList.AddRange(listC);
        sortedList.AddRange(listD);

        groupA.SetTeams(listA);
        groupB.SetTeams(listB);
        groupC.SetTeams(listC);
        groupD.SetTeams(listD);

        return sortedList;
    }

    public void ResetGroups()
    {
        groupA.SetTeams(groupAList);
        groupB.SetTeams(groupBList);
        groupC.SetTeams(groupCList);
        groupD.SetTeams(groupDList);
    }

    private void SetGroups()
    {
        groupAList = new List<Team>();
        groupBList = new List<Team>();
        groupCList = new List<Team>();
        groupDList = new List<Team>();

        for (int i = 0; i < teams.Length; i++)
        {
            if (i < 7)
            {
                groupAList.Add(teams[i]);
            }
            else if (i < 14)
            {
                groupBList.Add(teams[i]);
            }
            else if (i < 21)
            {
                groupCList.Add(teams[i]);
            }
            else
            {
                groupDList.Add(teams[i]);
            }
        }

        groupA.SetTeams(groupAList);
        groupB.SetTeams(groupBList);
        groupC.SetTeams(groupCList);
        groupD.SetTeams(groupDList);
    }

    public Group GetGroupByTeamNumber(int teamNumber)
    {
        if (teamNumber < 8)
        {
            return groupA;
        }
        else if (teamNumber < 15)
        {
            return groupB;
        }
        else if (teamNumber < 22)
        {
            return groupC;
        }
        else
        {
            return groupD;
        }
    }
}
