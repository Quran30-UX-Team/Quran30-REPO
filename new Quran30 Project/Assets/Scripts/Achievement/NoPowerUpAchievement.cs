using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoPowerUpAchievement : MonoBehaviour
{
    private AudioManager audioManager;
    public string achievementName;
    public string achievementDesc;
    public int requiredSurahCount = 5;

    private int correctAnswerCount = 0; // Counter for correct answers without power-ups
    private bool isPowerUpUsed = false; // Track if power-up is used in current surah
    private bool isCompleted = false; // Track if achievement is completed

    public TextMeshProUGUI achievementNameText;
    public TextMeshProUGUI achievementDescText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;
    public GameObject acceptPanel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        achievementNameText.text = achievementName;
        achievementDescText.text = achievementDesc;

        // Load completion state
        isCompleted = PlayerPrefs.GetInt("Achievement_" + achievementName + "_Completed", 0) == 1;

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

        UpdateExp();
    }

    public void OnAnswerSubmitted(bool isCorrect, bool powerUpUsed)
    {
        isPowerUpUsed = powerUpUsed;

        if (isCorrect && !isPowerUpUsed)
        {
            correctAnswerCount++;
            UpdateExp();
        }
    }

    public void UpdateExp()
    {
        // Display progress based on correct answer count
        float fillAmount = (float)correctAnswerCount / requiredSurahCount;
        progressFillbar.fillAmount = fillAmount;

        // Check if achievement is completed
        if (correctAnswerCount >= requiredSurahCount && isCompleted == false)
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
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementName + "_Accepted", 1); // Save acceptance state
        this.gameObject.tag = "Claimed";
    }
}