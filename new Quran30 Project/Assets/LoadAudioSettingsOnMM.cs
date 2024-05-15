using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadAudioSettingsOnMM : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        LoadVolume();
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume")) * 20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
    }

    private void LoadVolume()
    {
        SetBGMVolume();
        SetSFXVolume();
    }
}
