using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementPanelHandler : MonoBehaviour
{
    public string achievementName;
    public string achievementDescription;
    public int achievementLevel;

    private int achievementMaxExp;
    public TextMeshProUGUI achievementNameText;
    public TextMeshProUGUI achievementDescriptionText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;

    public GameObject acceptPanel;

    private bool isCompleted = false; // Track if achievement is completed

    private void Start()
    {
        achievementNameText.text = achievementName;
        achievementDescriptionText.text = achievementDescription;
        CalculateMaxExp();

        // Check if achievement was already completed and accepted
        if (PlayerPrefs.GetInt("Achievement_" + achievementName + "_Completed") == 1)
        {
            isCompleted = true;
            COMPLETED();

            // Check if achievement was accepted
            if (PlayerPrefs.GetInt("Achievement_" + achievementName + "_Accepted") == 1)
            {
                acceptAchievement();
            }
        }

        UpdateExp();
    }


    public void UpdateExp()
    {
        int currentLevel = PlayerPrefs.GetInt("playerLevel");

        float fillAmount = (float)PlayerPrefs.GetInt("currentExp") / achievementMaxExp;
        progressFillbar.fillAmount = fillAmount;

        if (!isCompleted && currentLevel >= achievementLevel)
        {
            isCompleted = true;
            COMPLETED();
            PlayerPrefs.SetInt("Achievement_" + achievementName + "_Completed", 1); // Save completion state
        }
    }

    private void CalculateMaxExp()
    {
        achievementMaxExp = 50 * achievementLevel; // Ensure achievementMaxExp is at least 1
    }

    public void COMPLETED()
    {
        progressContainer.SetActive(false);
        this.gameObject.tag = "CompletedAchievement";
    }

    public void acceptAchievement()
    {
        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementName + "_Accepted", 1); // Save acceptance state
        this.gameObject.tag = "Claimed";
    }
}
