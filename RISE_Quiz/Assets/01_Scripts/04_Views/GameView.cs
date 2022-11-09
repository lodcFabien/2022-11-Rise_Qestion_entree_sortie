using System;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text terminalText; 
    [SerializeField] private TMP_Text languageText;
    [SerializeField] private LanguageDropdownController languageDropdownController;
    [SerializeField] private TerminalDropdownController terminalDropdownController;

    [Header("Team Name")]
    [SerializeField] private TMP_Text teamNameText;

    public bool CorrectAnimationEnded { get; private set; } = false;
    public bool WrongAnimationEnded { get; private set; } = false;


    public void Init()
    {
        UpdateText(LocalizationManager.Instance.ActiveLanguage);
        LocalizationManager.Instance.OnLanguageChanged += UpdateText;
    }

    private void UpdateText(Language language)
    {
        var isFrench = language == Language.French;

        terminalText.text = isFrench ? "ID BORNE" : "TERMINAL ID";
        languageText.text = isFrench ? "LANGUE" : "LANGUAGE";

        languageDropdownController.Init();
        terminalDropdownController.Init();
    }

    public void SetTeamName(string name)
    {
        teamNameText.text = name;
    }

    public void DoCorrectAnimation()
    {
        CorrectAnimationEnded = false;
        //animator.SetTrigger("correct");
    }

    public void DoWrongAnimation()
    {
        WrongAnimationEnded = false;
        //animator.SetTrigger("wrong");
    }

    // Animation Events

    private void OnCorrectAnimationEnded() { CorrectAnimationEnded = true; }
    private void OnWrongAnimationEnded() { WrongAnimationEnded = true; }
}
