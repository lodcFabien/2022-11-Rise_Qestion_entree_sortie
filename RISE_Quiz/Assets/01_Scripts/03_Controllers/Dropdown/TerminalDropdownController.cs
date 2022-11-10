using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalDropdownController : DropdownController
{
    public override void Init()
    {
        base.Init();
        SetValue(0);
    }

    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetTerminalIDOptionsList(7, language));
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        GameManager.Instance.SetTerminalID(valueIndex + 1);
    }
}
