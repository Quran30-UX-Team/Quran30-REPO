using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlFatihahAchievement : MonoBehaviour
{
    public string achievementName = "Master of Al-Fatihah";
    public string achievementDesc = "You've mastered Surah Al-Fatihah with a score of 5 or higher!";

    public TextMeshProUGUI achievementNameText;
    public TextMeshProUGUI achievementDescText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;
    public GameObject acceptPanel;

    private int surahAlFatihahScore = 0; // Store the score for Surah Al-Fatihah

    private void Start()
    {
        // Initialize achievement UI
        achievementNameText.text = achievementName;
        achievementDescText.text = achievementDesc;

        // Load completion state
        bool isCompleted = PlayerPrefs.GetInt("Achievement_" + achievementName + "_Completed", 0) == 1;

        // Check if achievement was already completed and accepted
        if (isCompleted)
        {
            COMPLETED();

            // Check if achievement was accepted
            if (PlayerPrefs.GetInt("Achievement_" + achievementName + "_Accepted") == 1)
            {
                AcceptAchievement();
            }
        }
    }

    // Method called when the user earns a score for Surah Al-Fatihah
    public void OnSurahAlFatihahScoreEarned(int score)
    {
        surahAlFatihahScore = score;

        // Check if achievement is completed
        CheckCompletion();
    }

    // Method called to check if the achievement is completed
    private void CheckCompletion()
    {
        if (surahAlFatihahScore >= 5)
        {
            COMPLETED();
            PlayerPrefs.SetInt("Achievement_" + achievementName + "_Completed", 1); // Save completion state
        }
    }

    // Method called when the achievement is completed
    public void COMPLETED()
    {
        progressContainer.SetActive(false);
        this.gameObject.tag = "CompletedAchievement";
    }

    // Method called when the player accepts the achievement
    public void AcceptAchievement()
    {
        // Play a sound effect or animation to celebrate the achievement
        //...

        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementName + "_Accepted", 1); // Save acceptance state
        this.gameObject.tag = "Claimed";
    }

}