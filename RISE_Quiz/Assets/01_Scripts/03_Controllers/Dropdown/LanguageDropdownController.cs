using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageDropdownController : DropdownController
{
    public override int SetDefaultValue()
    {
        return (int)Language.French;
    }

    public override void SetOptions(Language language)
    {
        SetDropdownOptions(UIUtils.GetLanguageOptionsList(language));
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
