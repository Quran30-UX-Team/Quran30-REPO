using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class halvesChoice : MonoBehaviour
{
    private answerButton answerButton;
    public GameObject blockPanel;
    AudioManager audioManager;

    public Button powerUPBtn;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        blockPanel = GameObject.Find("PUBlockPanel");
    }

    private void Start()
    {
        answerButton = FindObjectOfType<answerButton>();
    }
    public void halfWrongAnswer(int amountToRemove)
    {
        blockPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        answerButton.RemoveRandomWrongAnswers(amountToRemove);
        powerUPBtn.interactable = false;
    }

}
