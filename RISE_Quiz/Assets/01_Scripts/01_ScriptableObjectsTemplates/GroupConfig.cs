using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Config/Group")]
public class GroupConfig : ScriptableObject
{
    [SerializeField] private GroupLetter group;
    [SerializeField] private List<Team> teams;

    public GroupLetter GroupLetter => group;
    public List<Team> Teams => teams;

    public void Init()
    {
        for(int i = 0; i < teams.Count; i++)
        {
            teams[i].Init(i);
        }
    }

    public List<Team> GetSortedTeams(int[] teamOrder)
    {
        var temp = new List<Team>();

        for (int i = 0; i < teamOrder.Length; i++)
        {
            temp.Add(teams.Find(x => x.ID == teamOrder[i]));
        }

        return temp;
    }
}
