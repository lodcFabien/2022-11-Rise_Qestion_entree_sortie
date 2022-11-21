using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager Instance { get; private set; }
    private void InitializeSingleton()
    {
        Instance = this;
    }

    #endregion

    [Header("AudioFile")]
    [SerializeField] private AudioFile file;

    [Header("AudioSources")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource ambiance;
    [SerializeField] private AudioSource fx;

    [Header("Settings")]
    [SerializeField] private float volumeStep = 0.1f;
    [SerializeField] private float timeStep = 0.1f;

    private float lastMusicVolume;
    private float lastAmbianceVolume;

    #region Unity Messages

    private void Awake()
    {
        InitializeSingleton();
    }

    #endregion

    private AudioSource GetAudioSourceByType(AudioType type)
    {
        switch (type)
        {
            case AudioType.Music:
                return music;

            case AudioType.Ambiance:
                return ambiance;

            case AudioType.FX:
                return fx;

            default:
                Debug.LogWarning("No Audiosource found.");
                return null;
        }
    }

    private string GetParamByType(AudioType type)
    {
        switch (type)
        {
            default:
                return "";

            case AudioType.Master:
                return "MasterVolume";

            case AudioType.Music:
                return "MusicVolume";

            case AudioType.Ambiance:
                return "AmbianceVolume";

            case AudioType.FX:
                return "FXVolume";
        }
    }

    public void SetVolume(AudioType audioSource, float volume)
    {
        GetAudioSourceByType(audioSource).volume = volume;
    }

    //public void PlayMainAmbiance()
    //{
    //    PlayAmbiance(file.ambiance);
    //}

    public void PlayMainMusic()
    {
        PlayMusic(file.mainMusic);
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void StopFX()
    {
        fx.Stop();
    }

    public void PlayCorrect(float volumeMultiplier = 1f)
    {
        PlaySoundEffectWithVolume(file.correct, volumeMultiplier);
    }

    public void PlayIncorrect(float volumeMultiplier = 1f)
    {
        PlaySoundEffectWithVolume(file.incorrect, volumeMultiplier);
    }

    public void PlayHint(float volumeMultiplier = 1f)
    {
        PlaySoundEffectWithVolume(file.hint, volumeMultiplier);
    }

    public void PlayVerify(bool isCorrect, float volumeMultiplier = 1f)
    {
        if (isCorrect)
            PlayCorrect(volumeMultiplier);
        else
            PlayIncorrect(volumeMultiplier);
    }

    public void PlaySelect(float volumeMultiplier = 1f)
    {
        PlaySoundEffectWithVolume(file.select, volumeMultiplier);
    }

    public void PlayConfirm(float volumeMultiplier = 1f)
    {
        PlaySoundEffectWithVolume(file.confirm, volumeMultiplier);
    }

    private void PlaySoundEffectWithVolume(AudioClip clip, float volumeScale = 1f)
    {
        PlaySoundEffect(clip, volumeScale);
    }


    private void PlayMusic(AudioClip clip)
    {
        music.Stop();
        music.clip = clip;
        music.Play();
        music.loop = true;
    }

    private void PlayAmbiance(AudioClip clip)
    {
        ambiance.Stop();
        ambiance.clip = clip;
        ambiance.Play();
        ambiance.loop = true;
    }

    private void PlaySoundEffect(AudioClip clip, float volumeScale = 1f)
    {
        Debug.Log($"Playing {clip}");
        fx.PlayOneShot(clip, volumeScale);
    }

    private IEnumerator VolumeDownCoroutine(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= volumeStep;
            yield return new WaitForSeconds(timeStep);
        }
        source.Stop();
    }

    private IEnumerator VolumeUpCoroutine(AudioSource source, float lastSourceVolume)
    {
        source.Play();
        while (source.volume < lastSourceVolume)
        {
            source.volume += volumeStep;
            yield return new WaitForSeconds(timeStep);
        }
    }

    public void TurnMusicVolumeDown()
    {
        lastMusicVolume = music.volume;
        StartCoroutine(VolumeDownCoroutine(music));
    }

    public void TurnAmbianceVolumeDown()
    {
        lastAmbianceVolume = ambiance.volume;
        StartCoroutine(VolumeDownCoroutine(ambiance));
    }

    public void TurnMusicVolumeUp()
    {
        StartCoroutine(VolumeUpCoroutine(music, lastMusicVolume));
    }

    public void TurnAmbianceVolumeUp()
    {
        StartCoroutine(VolumeUpCoroutine(ambiance, lastAmbianceVolume));
    }
}
public enum AudioType
{
    Master,
    Music,
    Ambiance,
    FX
}
