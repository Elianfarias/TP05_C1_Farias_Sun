using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    [SerializeField] private AudioSource soundEffectAudioSource;
    [SerializeField] private AudioSource BackgroundAudioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundEffect(AudioClip audioClip, bool priority = false)
    {
        if (soundEffectAudioSource.isPlaying && !priority)
            return;

        soundEffectAudioSource.clip = audioClip;
        soundEffectAudioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        BackgroundAudioSource.Stop();
    }

    public void PlayBackgroundMusic()
    {
        BackgroundAudioSource.Play();
    }
}
