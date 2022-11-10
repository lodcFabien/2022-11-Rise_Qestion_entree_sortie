using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Config/Group")]
public class Group : ScriptableObject
{
    [SerializeField] private GroupLetter group;
    [SerializeField] private List<Team> teams;

    public GroupLetter GroupLetter => group;
    public List<Team> Teams => teams;

    //public void Init()
    //{
    //    for(int i = 0; i < teams.Count; i++)
    //    {
    //        teams[i].Init(i);
    //    }
    //}
}
