using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDropdownController : DropdownController
{
    public override void Init()
    {
        base.Init();
        GameManager.Instance.OnGroupChanged += UpdateOptions;
    }

    private void UpdateOptions(GroupConfig config)
    {
        SetDropdownOptions(UIUtils.GetTeamOptionsList(config));
    }

    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetTeamOptionsList(GameManager.Instance.GetCurrentGroup()));
        SetValue(0);
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        GameManager.Instance.SetTeam(valueIndex);
    }
}
