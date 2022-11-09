using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageDropdownController : DropdownController
{
    public override void Init()
    {
        SetOptions();
    }

    public override void SetOptions()
    {
        SetDropdownOptions(UIUtils.GetLanguageOptionsList(LocalizationManager.Instance.ActiveLanguage));
    }

    public override void SetValue(int valueIndex)
    {
        if (dropdown.value != valueIndex)
        {
            dropdown.value = valueIndex;
        }

        Language language = (Language)valueIndex;
        LocalizationManager.Instance.SetLanguage(language);
    }
}
