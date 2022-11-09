using UnityEngine;

public abstract class Answer
{
    [SerializeField] protected LocalizationData dataText;
    public abstract bool IsCorrect();
    public abstract void ResetParameters();
    public string Text => dataText.GetTranslation(LocalizationManager.Instance.ActiveLanguage);
}
