using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text terminalText; 
    [SerializeField] private TMP_Text languageText;
    [SerializeField] private TMP_Text teamText;

    [Header("Team Name")]
    [SerializeField] private TMP_Text teamNameText;

    [Header("Dropdowns")]
    [SerializeField] private DropdownController[] dropdownControllers;

    [Header("Secret Buttons")]
    [SerializeField] private Button safranButton;
    [SerializeField] private Button riseButton;

    private bool canPressRise;

    public void Init()
    {
        UpdateText(LocalizationManager.Instance.ActiveLanguage);
        LocalizationManager.Instance.OnLanguageChanged += UpdateText;
        SetTeamName("");

        foreach(var dc in dropdownControllers)
        {
            dc.Init();
        }

        TurnOffRise();
    }

    private void UpdateText(Language language)
    {
        var isFrench = language == Language.French;

        terminalText.text = isFrench ? "ID BORNE" : "TERMINAL ID";
        languageText.text = isFrench ? "LANGUE" : "LANGUAGE";
        teamText.text = isFrench ? "ÉQUIPE" : "TEAM";
    }

    public void SetTeamName(string name)
    {
        teamNameText.text = name;
    }

    public void OnRiseButton()
    {
        if(!canPressRise)
        {
            return;
        }

        GameManager.Instance.ResetEverything();
    }

    public void OnSafranButton()
    {
        canPressRise = true;
        Invoke(nameof(TurnOffRise), 2f);
    }

    private void TurnOffRise()
    {
        canPressRise = false;
    }
}
