using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AudioFile")]
public class AudioFile : ScriptableObject
{
    [Header("Music & Ambiance")]
    public AudioClip mainMusic;
    //public AudioClip ambiance;

    [Header("Sound Effects")]
    //public AudioClip[] clips;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioClip hint;
    public AudioClip select;
    public AudioClip confirm;
}

[System.Serializable]
public struct AudioClipVolume
{
    public AudioClip clip;
    public float volume;
}
