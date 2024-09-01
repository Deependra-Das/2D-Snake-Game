using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public AudioSource audioSourceMenuSFX;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourcePowerupSFX;
    public AudioSource audioSourceFoodSFX;

    public AudioType[] AudioList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MuteAudioSource(AudioSourceList sourceName, bool value)
    {
        switch (sourceName)
        {
            case AudioSourceList.audioSourceMenuSFX:
                audioSourceMenuSFX.mute = value;
                break;

            case AudioSourceList.audioSourceBGM:
                audioSourceBGM.mute = value;
                break;

            case AudioSourceList.audioSourcePowerupSFX:
                audioSourcePowerupSFX.mute = value;
                break;

            case AudioSourceList.audioSourceFoodSFX:
                audioSourceFoodSFX.mute = value;
                break;

        }
    }

    public void PlayMenuSFX(AudioTypeList audio)
    {
        AudioClip clip = GetAudioClip(audio);
        if (clip != null)
        {
            audioSourceMenuSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audio);
        }
    }

    public void PlayBGM(AudioTypeList audio)
    {
        AudioClip clip = GetAudioClip(audio);
        if (clip != null)
        {
            audioSourceBGM.clip = clip;
            audioSourceBGM.Play();
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audio);
        }
    }

    public void PlayPowerupSFX(AudioTypeList audio)
    {
        AudioClip clip = GetAudioClip(audio);
        if (clip != null)
        {
            audioSourcePowerupSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audio);
        }
    }

    public void PlayFoodSFX(AudioTypeList audio)
    {
        AudioClip clip = GetAudioClip(audio);
        if (clip != null)
        {
            audioSourceFoodSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audio);
        }
    }

    public AudioClip GetAudioClip(AudioTypeList audio)
    {
        AudioType audioItem = Array.Find(AudioList, item => item.audioType == audio);
        if (audioItem != null)
        {
            return audioItem.audioClip;
        }
        return null;
    }

}

public enum AudioTypeList
{
    backgroundMusic,
    buttonMenuClick,
    massGainerFoodEaten,
    massBurnerFoodEaten,
    powerUpActivated,
    powerupDeactivated,
    death,
}

[Serializable]
public class AudioType
{
    public AudioTypeList audioType;
    public AudioClip audioClip;
}

public enum AudioSourceList
{
    audioSourceMenuSFX,
    audioSourceBGM,
    audioSourcePowerupSFX,
    audioSourceFoodSFX

}

