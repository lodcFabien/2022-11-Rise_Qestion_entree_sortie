using TMPro;
using UnityEngine;

public class HintPanelController : PanelController
{
    [SerializeField] protected GameObject spritePanel;

    [Header("Text")]
    [SerializeField] protected TMP_Text hintText;
    [SerializeField] protected TMP_Text hintNumber;
    [SerializeField] protected TMP_Text hintLetter;

    [Header("Animation")]
    [SerializeField] protected AnimationController spriteAnimator;
    [SerializeField] protected AnimationController waveAnimator;

    [Header("Confirm Button")]
    [SerializeField] protected GameObject confirmButton;

    private void Start()
    {
        spriteAnimator.OnEventTriggered += ActionOnSpriteEventTriggered;
        spriteAnimator.OnEndEventTriggered += ActionOnSpriteEndEventTriggered;
    }

    private void OnDestroy()
    {
        spriteAnimator.OnEventTriggered -= ActionOnSpriteEventTriggered;
        spriteAnimator.OnEndEventTriggered -= ActionOnSpriteEndEventTriggered;
    }

    public override void Toggle(bool toggle)
    {
        base.Toggle(toggle);
        spritePanel.SetActive(toggle);
        
        confirmButton.SetActive(false);
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
        spriteAnimator.ToggleAnim(true);
    }

    public void HideHint()
    {
        spriteAnimator.ToggleAnim(false);
    }

    private void ActionOnSpriteEventTriggered()
    {
        waveAnimator.TriggerAnim();
    }

    private void ActionOnSpriteEndEventTriggered()
    {
        confirmButton.SetActive(true);
    }

}
