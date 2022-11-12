using TMPro;
using UnityEngine;

public class HintPanelController : PanelController
{
    [Header("Text")]
    [SerializeField] protected TMP_Text hintText;
    [SerializeField] protected TMP_Text hintNumber;
    [SerializeField] protected TMP_Text hintLetter;

    [Header("Animation")]
    [SerializeField] protected GameObject spritePanel;
    [SerializeField] protected Animator animator;

    public override void Toggle(bool toggle)
    {
        base.Toggle(toggle);
        spritePanel.SetActive(toggle);
    }

    public void SetHint(string letter, Language language, int hintCount)
    {
        hintText.text = language == Language.French ? $"INDICE" : $"HINT";
        hintNumber.text = language == Language.French ? $"N°{hintCount}" : $"#{hintCount}";
        hintLetter.text = letter;

        ShowHint();
    }

    public void ShowHint()
    {
        animator.SetBool("visible", true);
    }

    public void HideHint()
    {
        animator.SetBool("visible", false);
    }
}
