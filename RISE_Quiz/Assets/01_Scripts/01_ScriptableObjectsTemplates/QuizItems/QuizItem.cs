using UnityEngine;

public abstract class QuizItem : ScriptableObject
{
    [SerializeField] protected LocalizationData dataText;

    public string Text => dataText.GetTranslation(LocalizationManager.Instance.ActiveLanguage);
}
