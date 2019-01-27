using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance = null;

    [System.Serializable]
    public class AudioSourcesInfo
    {
        public eSoundSourceType soundSourceType;

        public AudioSource audioSource;
    }

    [System.Serializable]
    public class SoundInfo
    {
        public eSoundType soundType;

        public AudioClip audioClip;
    }

    public AudioSourcesInfo[] audioSourcesInfos;
    public SoundInfo[] soundInfos;

    public AudioClip GetSoundClip(eSoundType soundType)
    {
        foreach (SoundInfo soundInfo in soundInfos)
        {
            if (soundInfo.soundType == soundType)
                return soundInfo.audioClip;
        }

        return null;
    }

    public AudioSource GetFreeAudioSource(eSoundSourceType soundSourceType = eSoundSourceType.SOUND_SOURCE_GENERAL)
    {
        foreach (AudioSourcesInfo audioSourcesInfo in audioSourcesInfos)
        {
            if (audioSourcesInfo.soundSourceType == soundSourceType && !audioSourcesInfo.audioSource.isPlaying)
            {
                return audioSourcesInfo.audioSource;
            }
        }

        return null;
    }

    public void PlaySound(eSoundType soundType, eSoundSourceType soundSourceType = eSoundSourceType.SOUND_SOURCE_GENERAL,
        float volume = 0.5f, float delay = 0f)
    {
        AudioClip clip = GetSoundClip(soundType);

        if (clip != null)
        {
            AudioSource freeAudioSource = GetFreeAudioSource(soundSourceType);

			if(freeAudioSource != null)
			{
				freeAudioSource.clip = clip;
                freeAudioSource.volume = volume;
				freeAudioSource.PlayDelayed(delay);
			}
        }
    }


    public void StopAllSounds()
    {
        foreach (AudioSourcesInfo audioSourcesInfo in audioSourcesInfos)
        {
            audioSourcesInfo.audioSource.Stop();
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
