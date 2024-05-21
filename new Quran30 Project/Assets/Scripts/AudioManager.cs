using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    public AudioClip BGM;

    public AudioClip changePageButtonSFX;
    public AudioClip correctAnswerSFX;
    public AudioClip wrongAnswerSFX;
    public AudioClip resultPageSFX;
    //public AudioClip claimAchievementSFX;
    //public AudioClip timerSFX;
    //public AudioClip powerupSFX;

    private void Start()
    {
        musicSource.clip = BGM;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
