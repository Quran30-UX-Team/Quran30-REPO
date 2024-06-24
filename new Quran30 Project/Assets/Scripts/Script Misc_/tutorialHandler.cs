using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialHandler : MonoBehaviour
{
    public GameObject tutorMenu;
    public GameObject tutorialBG;
    public GameObject[] tutorialPanels;
    private int currentPanelIndex = 0;

    public GameObject cursor;
    public GameObject[] tutorAnimation;
    public string[] boolParameterNames; // Array to hold the names of the bool parameters
    public bool[] boolParameterValues; // Array to hold the values of the bool parameters

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasSkip") != 1)
        {
            Debug.Log("Has not skipped");
        }
        else
        {
            tutorMenu.SetActive(false);
            tutorialBG.SetActive(false);
        }
    }

    void Start()
    {
        // Deactivate all tutorial panels except the first one
        for (int i = 1; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(false);
        }

        // Deactivate all tutorial animations except the first one
        for (int j = 1; j < tutorAnimation.Length; j++)
        {
            tutorAnimation[j].SetActive(false);
        }

        // Initialize boolParameterValues to all false
        boolParameterValues = new bool[tutorAnimation.Length];
        for (int i = 0; i < boolParameterValues.Length; i++)
        {
            boolParameterValues[i] = false;
        }

    }

    void Update()
{
    // Ensure only the current panel is active
    for (int i = 0; i < tutorialPanels.Length; i++)
    {
        tutorialPanels[i].SetActive(i == currentPanelIndex);
    }

    // Activate current animation and set boolean parameters
    for (int j = 0; j < boolParameterValues.Length; j++)
    {
        tutorAnimation[j].SetActive(j == currentPanelIndex);
        Animator animator = tutorAnimation[j].GetComponent<Animator>();

        for (int k = 0; k < boolParameterValues.Length; k++)
        {
            animator.SetBool(boolParameterNames[k], boolParameterValues[k] = false);
        }

        animator.SetBool(boolParameterNames[j], boolParameterValues[j] = true);
    }
}
    public void OnContinueButtonClick()
    {
        // Increment the current panel index
        currentPanelIndex++;

        // If all panels have been shown, reset to the first panel and load the scene
        if (currentPanelIndex >= tutorialPanels.Length)
        {
            tutorMenu.SetActive(false);
            tutorialBG.SetActive(false);
            SceneManager.LoadScene("Play");
            if (SceneManager.GetActiveScene().name == "Play")
            {
                PlayerPrefs.SetInt("hasSkip", 1);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void OnSkipButtonClick()
    {
        tutorMenu.SetActive(false);
        tutorialBG.SetActive(false);
        PlayerPrefs.SetInt("hasSkip", 1);
        SceneManager.LoadScene("MainMenu");
    }
}
