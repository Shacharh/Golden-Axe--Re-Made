using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer; 

    public void SetMasterVolume(float level)
    {
        // setting master audio decibel level (logarithmic)
        AudioMixer.SetFloat("MasterVolume",Mathf.Log10(level) * 20f);
    }

    public void SetSoundFXVolume(float level)
    {
        // setting SoundFX audio decibel level (logarithmic)
        AudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        // setting Music audio decibel levwl (logarithmic)
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
    private void Awake()
    {
        DontDestroyOnLoad(AudioMixer);
    }
}
