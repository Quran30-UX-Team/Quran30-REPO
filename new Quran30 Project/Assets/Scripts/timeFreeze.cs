using System.Collections;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    public GameObject freezePanel;
    private timeController timeController;

    void Start()
    {
        timeController = FindObjectOfType<timeController>(); // Finds the TimeController component in the scene
        if (timeController == null)
        {
            Debug.LogError("TimeController component not found!");
        }
    }
    public void FreezeTimer(float setTimer)
    {
        if (PlayerPrefs.GetFloat("Premium") > 9)
        {
            PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") - 10);
            StartCoroutine(FreezeForAMoment(setTimer));
        }
        else
        {
            print("No Money");
        }
    }

    IEnumerator FreezeForAMoment(float seconds)
    {
        timeController.enabled = false;
        if (freezePanel != null)
            freezePanel.SetActive(true);
        Debug.Log("FREEZE");
        yield return new WaitForSeconds(seconds);
        Debug.Log("UNFREEZE");
        timeController.enabled = true;
        if (freezePanel != null)
            freezePanel.SetActive(false);
    }

}

