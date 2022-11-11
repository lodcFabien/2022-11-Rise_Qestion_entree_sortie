using TMPro;
using UnityEngine;

public class HintPanelController : PanelController
{
    [SerializeField] protected TMP_Text hintText;
    [SerializeField] protected TMP_Text hintNumber;
    [SerializeField] protected TMP_Text hintLetter;
    [SerializeField] protected Animator animator;

    public void SetHint(string letter, Language language, int hintCount)
    {
        hintText.text = language == Language.French ? $"INDICE" : $"HINT";
        hintNumber.text = language == Language.French ? $"N°{hintCount}" : $"#{hintCount}";
        hintLetter.text = letter;
    }
}
