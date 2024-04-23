using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataSaver : MonoBehaviour
{
    private void Awake()
    {
        // Check if the game has been run for the first time
        if (!PlayerPrefs.HasKey("Initialized"))
        {
            // Set initial player preferences
            PlayerPrefs.SetFloat("Deeds", 50);
            PlayerPrefs.SetFloat("Score", 0);

            PlayerPrefs.SetFloat("Premium", 80);

            PlayerPrefs.SetString("Question Set", "Questions1");
            PlayerPrefs.SetString("Profile Name", "Guest");
            PlayerPrefs.SetString("Level Type", "QuizSurahSelect");
            PlayerPrefs.SetFloat("Countdown", 0);
            PlayerPrefs.SetFloat("scoreTemp", 0);

            PlayerPrefs.SetInt("playerLevel", 0);
            PlayerPrefs.SetInt("currentExp", 1);
            PlayerPrefs.SetInt("currentMaxExp", 50);

            PlayerPrefs.SetInt("choosenTheme", 1);

            PlayerPrefs.SetInt("hasSkip", 0);

            // Mark as initialized
            PlayerPrefs.SetInt("Initialized", 1);

            // Save the changes
            PlayerPrefs.Save();


            Debug.Log("Player preferences initialized.");
        }
        else
        {
            Debug.Log("Player preferences already initialized.");
        }
    }

}
