using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/Data")]
public class Data : ScriptableObject
{
    [SerializeField] private List<TerminalConfig> terminals;
    [SerializeField] private List<GroupConfig> groups;
    [SerializeField] private string[] wordsToFind;

    public void Init()
    {
        groups.ForEach(x => x.Init());
        terminals.ForEach(x => x.Init(wordsToFind));
    }

    public TerminalConfig GetTerminalFromID(int id)
    {
        return terminals.Find(x => x.ID == id);
    }

    public GroupConfig GetGroupFromLetter(GroupLetter letter)
    {
        return groups.Find(x => x.GroupLetter == letter);
    }
}
