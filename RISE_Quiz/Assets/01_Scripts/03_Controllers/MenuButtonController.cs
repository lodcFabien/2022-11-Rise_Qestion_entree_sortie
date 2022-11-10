using Newtonsoft.Json.Serialization;
using UnityEngine;

public class MenuButtonController : ButtonController
{
    [SerializeField] protected LocalizationData localizationData;

    protected MenuButtonView View => view as MenuButtonView;
    protected bool hasDoneInit = false;

    private void OnEnable()
    {
        if(!hasDoneInit)
        {
            Init();
        }
    }

    protected virtual void Init()
    {
        LocalizationManager.Instance.OnLanguageChanged += UpdateButtonByLanguage;
        View.Populate(localizationData.GetTranslation(LocalizationManager.Instance.ActiveLanguage));
        View.OnClickAnimationEnded += ActionOnClickAnimComplete;
        hasDoneInit = true;
    }

    protected virtual void UpdateButtonByLanguage(Language language)
    {
        View.Populate(localizationData.GetTranslation(language));
    }

    public void OnClick()
    {
        View.OnClick();
    }

    protected void ActionOnClickAnimComplete()
    {
        GameManager.Instance.OnConfirmButton();
    }
}
