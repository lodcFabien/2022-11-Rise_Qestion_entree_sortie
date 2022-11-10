using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class DropdownController : MonoBehaviour
{
    [SerializeField] protected TMP_Dropdown dropdown;

    public virtual void Init()
    {
        SetOptions(LocalizationManager.Instance.ActiveLanguage);
        LocalizationManager.Instance.OnLanguageChanged += UpdateOptions;
    }

    public virtual void UpdateOptions(Language language)
    {
        SetOptions(language);
    }

    public abstract void SetOptions(Language language);
    public abstract void SetValue(int valueIndex);

    public void SetDropdownOptions(List<string> options)
    {
        int currentValue = dropdown.value;
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.value = currentValue;
        dropdown.RefreshShownValue();
    }
}
