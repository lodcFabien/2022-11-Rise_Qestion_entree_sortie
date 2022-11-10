using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/Data")]
public class Data : ScriptableObject
{
    [SerializeField] private List<TerminalConfig> terminals;
    [SerializeField] private List<Group> groups;
    [SerializeField] private List<MultipleChoiceQuestion> questions;
    [SerializeField] private string[] wordsToFind;

    public void Init()
    {
        terminals.ForEach(x => x.Init(questions, wordsToFind));
        groups.ForEach(x => x.Init());
    }

    public TerminalConfig GetTerminalFromID(int id)
    {
        return terminals.Find(x => x.ID == id);
    }

    public Group GetGroupFromLetter(GroupLetter letter)
    {
        return groups.Find(x => x.GroupLetter == letter);
    }
}
