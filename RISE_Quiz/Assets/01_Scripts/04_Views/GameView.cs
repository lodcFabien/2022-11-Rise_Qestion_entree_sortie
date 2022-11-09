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

    //[Header("Confirm Button Text")]
    //[SerializeField] private TMP_Text confirmButtonText;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    public bool WaitAnimationEnded { get; private set; } = false;
    public bool CorrectAnimationEnded { get; private set; } = false;
    public bool WrongAnimationEnded { get; private set; } = false;


    private void Awake()
    {
        LocalizationManager.Instance.OnLanguageChanged += UpdateText;
    }

    private void UpdateText(Language language)
    {
        var isFrench = language == Language.French;

        terminalText.text = isFrench ? "ID BORNE" : "TERMINAL ID";
        languageText.text = isFrench ? "LANGUAGE" : "LANGUE";

        languageDropdownController.Init();
        terminalDropdownController.Init();
    }

    public void SetTeamName(string name)
    {
        teamNameText.text = name;
    }

    //public void SetConfirmButton(QuizState state)
    //{
    //    var isFrench = LocalizationManager.Instance.ActiveLanguage == Language.French;

    //    switch (state)
    //    {
    //        case QuizState.Setup:
    //        case QuizState.EntryQuestion:
    //        case QuizState.ExitQuestion:
    //            confirmButtonText.text = isFrench ? "VALIDER" : "CONFIRM";
    //            break;
    //        case QuizState.WaitingForStart:
    //            confirmButtonText.text = isFrench? "COMMENCER" : "START";
    //            break;
    //        case QuizState.WaitingForExpertSpeech:
    //        case QuizState.VerifyingAnswer:
    //            confirmButtonText.text = isFrench ? "CONTINUER" : "CONTINUE";
    //            break;
    //        case QuizState.DisplayingHint:
    //            confirmButtonText.text = isFrench ? "OK" : "OKAY";
    //            break;
    //        case QuizState.Ended:
    //            confirmButtonText.text = isFrench ? "TERMINER" : "FINISH";
    //            break;
    //    }
    //}

    public void DoWaitAnimation()
    {
        WaitAnimationEnded = false;
        animator.SetTrigger("wait");
    }

    public void DoCorrectAnimation()
    {
        CorrectAnimationEnded = false;
        animator.SetTrigger("correct");
    }

    public void DoWrongAnimation()
    {
        WrongAnimationEnded = false;
        animator.SetTrigger("wrong");
    }

    // Animation Events

    private void OnWaitAnimationEnded() { WaitAnimationEnded = true; }
    private void OnCorrectAnimationEnded() { CorrectAnimationEnded = true; }
    private void OnWrongAnimationEnded() { WrongAnimationEnded = true; }
}
