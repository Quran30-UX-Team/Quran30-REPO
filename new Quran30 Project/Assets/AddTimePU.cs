using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddTimePU : MonoBehaviour
{
    private timeController timeController;
    public Button powerUPBtn;

    void Start()
    {
        timeController = FindObjectOfType<timeController>(); // Finds the TimeController component in the scene
        if (timeController == null)
        {
            Debug.LogError("TimeController component not found!");
        }

    }

    public void AddTime(float AddTime)
    {
        powerUPBtn.interactable = false;
        timeController.AddTime(AddTime); // Call AddTime method in timeController and pass the AddTime value
    }
}
