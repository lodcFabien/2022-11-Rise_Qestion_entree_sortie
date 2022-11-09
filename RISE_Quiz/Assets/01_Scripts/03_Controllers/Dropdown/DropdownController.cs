using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class DropdownController : SaveableComponent
{
    [SerializeField] protected TMP_Dropdown dropdown;
    public abstract void SetOptions();
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
