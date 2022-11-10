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

    private void UpdateOptions(Group config)
    {
        SetDropdownOptions(UIUtils.GetTeamOptionsList(config));
    }

    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetTeamOptionsList(GameManager.Instance.GetCurrentGroup()));
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        GameManager.Instance.SetTeam(valueIndex);
    }

    public override int SetDefaultValue()
    {
        return 0;
    }
}
