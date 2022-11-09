using UnityEngine;

public class MenuButtonController : ButtonController
{
    [SerializeField] protected LocalizationData localizationData;

    public virtual void Init()
    {
        LocalizationManager.Instance.OnLanguageChanged += UpdateButtonByLanguage;
        //GameManager.Instance.OnQuizStateChanged += ActionOnQuizStateChanged;
        view.Populate(localizationData.GetTranslation(LocalizationManager.Instance.ActiveLanguage));
    }

    protected virtual void UpdateButtonByLanguage(Language language)
    {
        view.Populate(localizationData.GetTranslation(language));
    }

    //protected virtual void ActionOnQuizStateChanged(QuizState newState)
    //{
    //    switch (newState)
    //    {
    //        case QuizState.Init:
    //            break;
    //        case QuizState.Setup:
    //            break;
    //        case QuizState.WaitingForStart:
    //            break;
    //        case QuizState.EntryQuestion:
    //            break;
    //        case QuizState.WaitingForExpertSpeech:
    //            break;
    //        case QuizState.ExitQuestion:
    //            break;
    //        case QuizState.VerifyingAnswer:
    //            break;
    //        case QuizState.DisplayingHint:
    //            break;
    //        case QuizState.Ended:
    //            break;
    //    }
    //}
}
