using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTimerEnd : MonoBehaviour
{
    public GameObject blockPanel;
    private timeController timeController;

    private void Awake()
    {
        timeController = FindObjectOfType<timeController>();
        if (timeController == null)
        {
            Debug.LogError("timeController not found in the scene!");
        }
    }
    void Update()
    {
        if (timeController.fillBar.fillAmount <= 0)
        {
            blockPanel.SetActive(false);
        }
    }
}
