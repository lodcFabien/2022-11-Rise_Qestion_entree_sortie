using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambleController : MonoBehaviour
{
    [Header("Scramble Settings")]
    [SerializeField] protected bool doScramble = true;
    [SerializeField] protected float scrambleWaitTime = 7f;
    [SerializeField] protected float scrambleStayTime = 2.5f;
    [SerializeField] protected AnimationController animator;

    [Header("Animation")]
    [SerializeField] protected List<TextScrambler> scramblers;
    [SerializeField] protected List<TextWobbler> wobblers;

    protected void Start()
    {
        StartCoroutine(ScrambleCoroutine());
    }

    private IEnumerator ScrambleCoroutine()
    {
        while (doScramble)
        {
            yield return new WaitForSeconds(scrambleWaitTime);
            animator.ToggleAnim(true);
            scramblers.ForEach(x => x.Scramble());
            yield return new WaitForSeconds(scrambleStayTime);
            animator.ToggleAnim(false);
            scramblers.ForEach(x => x.Unscramble());
        }
    }
}
