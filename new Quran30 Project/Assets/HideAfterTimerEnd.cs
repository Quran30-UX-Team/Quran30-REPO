using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTimerEnd : MonoBehaviour
{
    public GameObject blockPanel;
    private questionSetup questionSetup;
    private int previousCounterValue;

    private void Awake()
    {
        questionSetup = FindObjectOfType<questionSetup>();
        if (questionSetup == null)
        {
            Debug.LogError("questionSetup not found in the scene!");
        }
    }

    private void Start()
    {
        // Initialize previousCounterValue with the initial value of counter
        previousCounterValue = questionSetup.counter;
    }

    void Update()
    {
        // Check if counter value has changed
        if (questionSetup.counter != previousCounterValue)
        {
            blockPanel.SetActive(false);
            // Counter has changed, do something here
            Debug.Log("Counter has changed from " + previousCounterValue + " to " + questionSetup.counter);

            // Update previousCounterValue to the current value
            previousCounterValue = questionSetup.counter;
        }
    }
}
