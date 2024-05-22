using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnsBtnColor2 : MonoBehaviour
{
    private questionSetup questionSetup;
    private int lastCounterValue;

    public GameObject AnsBtn2;
    public GameObject TargetObject;

    public void Start()
    {
        questionSetup = GameObject.FindObjectOfType<questionSetup>();
        if (questionSetup != null)
        {
            lastCounterValue = questionSetup.counter; // Initialize the last counter value
        }
        else
        {
            Debug.LogError("questionSetup object not found!");
        }
    }

    private void Update()
    {
        while (true)
        {
            if (questionSetup.counter != lastCounterValue)
            {
                AnsBtn2.GetComponent<Image>().color = TargetObject.GetComponent<Image>().color;
            }
        }
    }
}
