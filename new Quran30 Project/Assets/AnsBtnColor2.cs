using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnsBtnColor2 : MonoBehaviour
{
    private questionSetup questionSetup;
    private int lastCounterValue;
    private Color lastColor;

    public GameObject AnsBtn2;
    public GameObject TargetObject;
    public float checkInterval = 1f; // Interval in seconds

    public void Start()
    {
        questionSetup = GameObject.FindObjectOfType<questionSetup>();
        if (questionSetup != null)
        {
            lastCounterValue = questionSetup.counter; // Initialize the last counter value
            StartCoroutine(CheckCounter());
        }
        else
        {
            Debug.LogError("questionSetup object not found!");
        }
    }

    private IEnumerator CheckCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (questionSetup.counter != lastCounterValue)
            {
                Debug.Log("Counter value changed from " + lastCounterValue + " to " + questionSetup.counter);
                lastCounterValue = questionSetup.counter; // Update the last counter value
                Color newColor = TargetObject.GetComponent<Image>().color;
                if (newColor != lastColor)
                {
                    lastColor = newColor;
                    AnsBtn2.GetComponent<Image>().color = newColor;
                }
            }
        }
    }
}
