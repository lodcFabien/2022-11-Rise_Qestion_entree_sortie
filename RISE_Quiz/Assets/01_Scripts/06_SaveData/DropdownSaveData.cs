using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownSaveData : SaveData
{
    [SerializeField] private TMP_Dropdown dropdown;

    protected override void DoLoad()
    {
        dropdown.value = PlayerPrefs.GetInt(settingName, 0);
        dropdown.RefreshShownValue();
        dropdown.onValueChanged.Invoke(dropdown.value);
    }

    protected override void DoSave()
    {
        PlayerPrefs.SetInt(settingName, dropdown.value);
    }
}
