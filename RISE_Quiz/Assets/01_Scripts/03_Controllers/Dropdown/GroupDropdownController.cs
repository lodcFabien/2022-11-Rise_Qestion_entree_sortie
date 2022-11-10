using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDropdownController : DropdownController
{
    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetGroupOptionsList());
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        GroupLetter group = (GroupLetter)valueIndex;
        GameManager.Instance.SetGroup(group);
    }
}
