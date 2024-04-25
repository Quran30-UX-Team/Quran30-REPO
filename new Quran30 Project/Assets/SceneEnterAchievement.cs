using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneEnterAchievement : MonoBehaviour
{
    public string achievementName;
    public int requiredSceneEntries = 1; // Number of scene entries required to complete the achievement

    private int sceneEntriesCount = 0; // Counter for scene entries
    private bool isCompleted = false; // Track if achievement is completed

    public TextMeshProUGUI achievementNameText;
    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;
    public GameObject acceptPanel;

    private void Start()
    {
        achievementNameText.text = achievementName;

        // Load completion state
        isCompleted = PlayerPrefs.GetInt("Achievement_" + achievementName + "_Completed", 0) == 1;

        // Load scene entries count from PlayerPrefs
        sceneEntriesCount = PlayerPrefs.GetInt("SceneEntriesCount", 0);

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

    private void Update()
    {
        // Display progress based on scene entries count
        float fillAmount = (float)sceneEntriesCount / requiredSceneEntries;
        if (fillAmount < 0.1f)
        {
            print("Empty");
        }
        else
        {
            progressFillbar.fillAmount = fillAmount;
        }

        // Check if achievement is completed
        if (sceneEntriesCount >= requiredSceneEntries)
        {
            isCompleted = true;
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
        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementName + "_Accepted", 1); // Save acceptance state
    }
}