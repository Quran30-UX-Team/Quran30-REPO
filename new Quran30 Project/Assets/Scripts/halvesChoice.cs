using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class halvesChoice : MonoBehaviour
{
    private answerButton answerButton;

    public Button powerUPBtn;

    private void Start()
    {
        answerButton = FindObjectOfType<answerButton>();
    }
    public void halfWrongAnswer(int amountToRemove)
    {
        answerButton.RemoveRandomWrongAnswers(amountToRemove);
        powerUPBtn.interactable = false;
    }
}
