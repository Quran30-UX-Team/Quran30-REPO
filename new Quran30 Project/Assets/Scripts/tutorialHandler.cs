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

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasSkip")!= 1)
        {
            Debug.Log("Has not skipped");
        }
        else
        {
            tutorMenu.SetActive(false);
            tutorialBG.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate all tutorial panels except the first one
        for (int i = 1; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Ensure only the current panel is active
        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(i == currentPanelIndex);
        }
    }

    // Event handler for the continue button click
    public void OnContinueButtonClick()
    {
        // Increment the current panel index
        currentPanelIndex++;

        // If all panels have been shown, reset to the first panel
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

