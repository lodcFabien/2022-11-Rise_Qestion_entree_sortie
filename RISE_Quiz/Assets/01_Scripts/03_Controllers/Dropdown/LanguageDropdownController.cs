using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageDropdownController : DropdownController
{
    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetLanguageOptionsList(language));
        SetValue(0);
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
