using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;
using UnityEngine.UI;

public class AdWatcherAchievement : MonoBehaviour
{
    public string achievementName = "Ad Watcher";
    public string achievementDesc = "Watch all ads without skipping!";
    public int requiredAdViews = 5; // Adjust this to the number of ads you want users to watch

    private int adViews = 0; // Counter for ad views
    private bool isAdSkipped = false; // Flag to track if ad is skipped

    public TextMeshProUGUI achievementNameText;
    public TextMeshProUGUI achievementDescText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;
    public GameObject acceptPanel;

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

    // Called when an ad is shown
    public void OnAdStarted()
    {
        isAdSkipped = false; // Reset skip flag
    }

    // Called when an ad is completed (not skipped)
    public void OnAdCompleted()
    {
        if (!isAdSkipped)
        {
            adViews++; // Increment ad view counter
            UpdateExp();
        }
    }

    // Called when an ad is skipped
    public void OnAdSkipped()
    {
        isAdSkipped = true; // Set skip flag
    }

    public void UpdateExp()
    {
        // Display progress based on ad views
        float fillAmount = (float)adViews / requiredAdViews;
        progressFillbar.fillAmount = fillAmount;

        // Check if achievement is completed
        if (adViews >= requiredAdViews)
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
        // ...

        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementName + "_Accepted", 1); // Save acceptance state
        this.gameObject.tag = "Claimed";
    }
}