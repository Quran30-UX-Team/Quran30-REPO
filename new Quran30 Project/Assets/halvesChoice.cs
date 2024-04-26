using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halvesChoice : MonoBehaviour
{
    private answerButton answerButton;

    private void Start()
    {
        answerButton = FindObjectOfType<answerButton>();
    }
    public void halfWrongAnswer()
    {
        answerButton.RemoveRandomWrongAnswers(2);
        this.gameObject.SetActive(false);
    }
}
