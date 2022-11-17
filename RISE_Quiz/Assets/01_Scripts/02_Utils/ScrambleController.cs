using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleController : MonoBehaviour
{
    [Header("Scramble Settings")]
    [SerializeField] protected bool doScramble = true;
    [SerializeField] protected float scrambleWaitTime = 7f;
    [SerializeField] protected float scrambleStayTime = 2.5f;
    [SerializeField] protected AnimationController animator;

    [Header("Animation")]
    [SerializeField] protected List<HorizontalLayoutGroup> hlgs;
    [SerializeField] protected List<TextScrambler> scramblers;
    [SerializeField] protected List<TextWobbler> wobblers;

    private Coroutine scramble;

    protected void Start()
    {
        StopScrambleCoroutine();
        scramblers.ForEach(x => x.Init(LocalizationManager.Instance.ActiveLanguage));
        StartScrambleCoroutine();
        LocalizationManager.Instance.OnLanguageChanged += UpdateLanguage;
        GameManager.Instance.OnQuizStateChanged += ActionOnQuizStateChanged;
    }

    private void StartScrambleCoroutine()
    {
        scramble = StartCoroutine(ScrambleCoroutine());
    }

    private void OnDestroy()
    {
        LocalizationManager.Instance.OnLanguageChanged -= UpdateLanguage;
        GameManager.Instance.OnQuizStateChanged -= ActionOnQuizStateChanged;
    }

    protected void ActionOnQuizStateChanged(QuizState newState)
    {
        StopScrambleCoroutine();

        switch (newState)
        {
            case QuizState.Init:
            case QuizState.Setup:
            case QuizState.WaitingForStart:
            case QuizState.DisplayingHint:
            case QuizState.WaitingForExpertSpeech:
                if(scramble == null)
                {
                    StartScrambleCoroutine();
                }
                break;

            case QuizState.EntryQuestion:
            case QuizState.ExitQuestion:
            case QuizState.Verifying:
                StopScrambleCoroutine();
                break;
        }
    }

    protected void UpdateLanguage(Language language)
    {
        StopScrambleCoroutine();
        scramblers.ForEach(x => x.UpdateLanguage(language));
        StartScrambleCoroutine();
    }

    private void DeactivateHLGs()
    {
        hlgs.ForEach(x => x.enabled = false);
    }

    private void ReactivateHLGs()
    {
        hlgs.ForEach(x => x.enabled = true);
    }

    private void StopScrambleCoroutine()
    {
        wobblers.ForEach(x => x.StopWobble());

        if (scramble != null)
        {
            StopCoroutine(scramble);
            scramble = null;
            scramblers.ForEach(x => x.Unscramble());
            animator.ToggleAnim(false);
        }
    }

    private IEnumerator ScrambleCoroutine()
    {
        ReactivateHLGs();
        yield return new WaitForSeconds(.5f);
        DeactivateHLGs();
        wobblers.ForEach(x => x.StartWobble());

        while (doScramble)
        {
            animator.ToggleAnim(true);
            scramblers.ForEach(x => x.Scramble());
            yield return new WaitForSeconds(scrambleStayTime);
            animator.ToggleAnim(false);
            scramblers.ForEach(x => x.Unscramble());
            yield return new WaitForSeconds(scrambleWaitTime);
        }
    }
}
