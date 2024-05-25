using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStop : MonoBehaviour
{
    private timeController timeController;
    private questionSetup questionSetup;
    private int previousQuestionCount = 0;

    private void Awake()
    {
        timeController = FindObjectOfType<timeController>(); // Finds the TimeController component in the scene
        if (timeController == null)
        {
            Debug.LogError("TimeController component not found!");
        }

        questionSetup = FindObjectOfType<questionSetup>(); // Finds the QuestionSetup component in the scene
        if (questionSetup == null)
        {
            Debug.LogError("QuestionSetup component not found!");
        }
    }

    private void Update()
    {
        int currentQuestionCount = questionSetup.questions.Count;

        if (currentQuestionCount != previousQuestionCount)
        {
            if (currentQuestionCount > 0)
            {
                timeController.enabled = true;
            }
            else
            {
                timeController.enabled = false;
            }

            // Update the previous count
            previousQuestionCount = currentQuestionCount;
        }
    }

    public void SS()
    {
        if (timeController.enabled == true)
        {
            timeController.enabled = false;
        }
        else if (timeController.enabled == false)
        {
            timeController.enabled = true;
        }
    }
}
