using System.Collections;
using UnityEngine;

public class WaitPanelController : PanelController
{
    [SerializeField] protected AnimationController waveController;
    [SerializeField] protected float waveTimer;

    protected Coroutine wave;

    protected void OnEnable()
    {
        StartWaveCoroutine();
    }

    protected void OnDisable()
    {
        StopWaveCoroutine();
    }

    protected void StartWaveCoroutine()
    {
        StopWaveCoroutine();

        wave = StartCoroutine(WaveCoroutine());
    }


    protected void StopWaveCoroutine()
    {
        if (wave != null)
        {
            StopCoroutine(wave);
            wave = null;
        }
    }

    protected IEnumerator WaveCoroutine()
    {
        while(true)
        {
            waveController.TriggerAnim();
            yield return new WaitForSeconds(waveTimer);
        }
    }
}
