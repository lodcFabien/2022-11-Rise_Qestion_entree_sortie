using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDropdownController : DropdownController
{
    public override void Init()
    {
        base.Init();
        SetValue(0);
        GameManager.Instance.OnTeamChanged += UpdateValue;
    }

    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetTeamOptionsList(GameManager.Instance.Teams));
    }

    private void UpdateValue(int teamIndex)
    {
        dropdown.SetValueWithoutNotify(teamIndex);
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        GameManager.Instance.SetTeamByDropdown(valueIndex);
    }
}
