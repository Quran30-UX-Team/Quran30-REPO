using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddTimePU : MonoBehaviour
{
    private timeController timeController;
    public GameObject blockPanel;
    public Button powerUPBtn;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        blockPanel = GameObject.Find("PUBlockPanel");
    }

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
        blockPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        powerUPBtn.interactable = false;
        timeController.AddTime(AddTime); // Call AddTime method in timeController and pass the AddTime value
    }

}
