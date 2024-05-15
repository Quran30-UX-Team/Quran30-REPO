using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class halvesChoice : MonoBehaviour
{
    private answerButton answerButton;
    AudioManager audioManager;

    public Button powerUPBtn;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        answerButton = FindObjectOfType<answerButton>();
    }
    public void halfWrongAnswer(int amountToRemove)
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        answerButton.RemoveRandomWrongAnswers(amountToRemove);
        powerUPBtn.interactable = false;
    }
}
