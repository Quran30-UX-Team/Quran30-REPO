using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeedCollectedAchievement : MonoBehaviour
{
    public string achievementName;

    public int totalDeedsToAchieve;
    public TextMeshProUGUI achievementNameText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;

    public GameObject acceptPanel;

    private bool isCompleted = false; // Track if achievement is completed

    private void Start()
    {
        achievementNameText.text = achievementName;

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

        float fillAmount = (float)PlayerPrefs.GetInt("totalDeed") / totalDeedsToAchieve;
        progressFillbar.fillAmount = fillAmount;

        if (!isCompleted && PlayerPrefs.GetInt("totalDeed") >= totalDeedsToAchieve)
        {
            isCompleted = true;
            COMPLETED();
            PlayerPrefs.SetInt("Achievement_" + achievementName + "_Completed", 1); // Save completion state
        }
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
