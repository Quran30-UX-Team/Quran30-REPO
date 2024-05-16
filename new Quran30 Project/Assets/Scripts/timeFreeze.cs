using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeFreeze : MonoBehaviour
{
    private GameObject freezePanel;
    private timeController timeController;
    AudioManager audioManager;

    public Button powerUPBtn;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        timeController = FindObjectOfType<timeController>(); // Finds the TimeController component in the scene
        if (timeController == null)
        {
            Debug.LogError("TimeController component not found!");
        }

        freezePanel = GameObject.Find("freezePanel");
    }
    public void FreezeTimer(float setTimer)
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        StartCoroutine(FreezeForAMoment(setTimer));
    }

    IEnumerator FreezeForAMoment(float seconds)
    {
        powerUPBtn.interactable = false;
        timeController.enabled = false;
        if (freezePanel != null)
        {
            freezePanel.SetActive(true);
        }
        Debug.Log("FREEZE");
        yield return new WaitForSeconds(seconds);
        Debug.Log("UNFREEZE");
        timeController.enabled = true;
        if (freezePanel != null)
        {
            freezePanel.SetActive(false);
        }
    }

}

