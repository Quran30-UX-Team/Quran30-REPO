using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class resultPage : MonoBehaviour
{
    public AudioManager audioManager;

    public ParticleSystem VFX;
    private LeaderboardManager leaderboard;

    public GameObject congratsPanel; // This should be an Image or GameObject with an Image component
    public GameObject deedsImage; // DeedsImage as parent

    private TextMeshProUGUI congratsPanelText;
    private TextMeshProUGUI rewardPanelText; // Text as child under DeedsImage

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public GameObject tryAgainBanner; // New GameObject for TRY AGAIN banner
    public GameObject congratulationsBanner; // New GameObject for CONGRATULATIONS banner

    public TextMeshProUGUI[] deedTexts;
    public TextMeshProUGUI[] scoreTexts;

    public TextMeshProUGUI totalDeed;
    public TextMeshProUGUI totalScore;

    public GameObject devPanel;

    public TextMeshProUGUI totalScoreText;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
    }

    public void Start()
    {
        PlayerPrefs.SetString("Slot1", "Empty");
        PlayerPrefs.SetString("Slot2", "Empty");
        PlayerPrefs.SetString("Slot3", "Empty");

        Debug.Log(PlayerPrefs.GetString("Slot1") + " " + PlayerPrefs.GetString("Slot2") + " " + PlayerPrefs.GetString("Slot3"));

        leaderboard = FindObjectOfType<LeaderboardManager>();
        congratsPanelText = congratsPanel.GetComponentInChildren<TextMeshProUGUI>();
        rewardPanelText = deedsImage.GetComponentInChildren<TextMeshProUGUI>();

        float totalDeeds = 0;
        float totalScores = 0;

        if (DevBtn.devMode != false)
        {
            devPanel.SetActive(true);
        }

        // Set deed and score texts dynamically
        for (int i = 0; i < deedTexts.Length; i++)
        {
            float deeds = PlayerPrefs.GetFloat("Deeds_" + (i + 1));
            float scores = PlayerPrefs.GetFloat("Scores_" + (i + 1));

            deedTexts[i].text = "Q" + (i + 1) + " Deeds: " + deeds;
            scoreTexts[i].text = "Q" + (i + 1) + " Scores: " + scores;

            totalDeeds += deeds;
            totalScores += scores;

            // Debug log to show the deeds and scores for each question
            Debug.Log("Question " + (i + 1) + " - Deeds: " + deeds + ", Scores: " + scores);
        }

        // Double the total deeds for RushSurahSelect mode
        if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect")
        {
            totalDeeds *= 2;
            totalScores *= 2;
        }

        totalDeed.text = "Total Deeds: " + totalDeeds.ToString();
        totalScore.text = "Total Scores: " + totalScores.ToString();

        // Check the score and activate stars accordingly
        if (totalScores < 0)
        {
            totalScores = 0;
        }

        if (totalScores < 0.04)
        {
            Star1.SetActive(false);
            Star2.SetActive(false);
            Star3.SetActive(false);
            tryAgainBanner.SetActive(true); // Activate TRY AGAIN banner
            congratulationsBanner.SetActive(false); // Deactivate CONGRATULATIONS banner
        }
        else if (totalScores < 15)
        {
            Star1.SetActive(true);
            Star2.SetActive(false);
            Star3.SetActive(false);
            tryAgainBanner.SetActive(false); // Deactivate TRY AGAIN banner
            congratulationsBanner.SetActive(true); // Activate CONGRATULATIONS banner

            audioManager.PlaySFX(audioManager.resultPageSFX);
        }
        else if (totalScores < 30)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(false);
            tryAgainBanner.SetActive(false); // Deactivate TRY AGAIN banner
            congratulationsBanner.SetActive(true); // Activate CONGRATULATIONS banner

            audioManager.PlaySFX(audioManager.resultPageSFX);
        }
        else
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
            tryAgainBanner.SetActive(false); // Deactivate TRY AGAIN banner
            congratulationsBanner.SetActive(true); // Activate CONGRATULATIONS banner

            audioManager.PlaySFX(audioManager.resultPageSFX);
        }

        // Activate Confetti only if at least one star is active
        if (Star1.activeSelf || Star2.activeSelf || Star3.activeSelf)
        {
            StartCoroutine(Confetti(0.1f));
        }

        if (Star1.activeSelf == true && Star2.activeSelf == true && Star3.activeSelf == true)
        {
            PlayerPrefs.SetInt("PerfectScore", PlayerPrefs.GetInt("PerfectScore") + 1);
        }

        string roundedScore = Mathf.RoundToInt(totalScores).ToString();
        string roundedDeed = Mathf.RoundToInt(totalDeeds).ToString();

        if (PlayerPrefs.GetString("Level Type") == "PracticeSurahSelect")
        {
            totalDeeds = 0;
            roundedDeed = 0 + "";
        }

        congratsPanelText.text = "<b>" + roundedScore + "</b>";
        rewardPanelText.text = roundedDeed ; // kena buang panel reward

        // Update the deeds value in PlayerPrefs
        float oldDeeds = PlayerPrefs.GetFloat("Deeds", 0); // Get the old deeds value, default to 0 if not set
        float newDeeds = totalDeeds; // Calculate the new deeds obtained
        PlayerPrefs.SetFloat("Deeds", oldDeeds + newDeeds);
        PlayerPrefs.Save(); // Save changes to PlayerPrefs


        int totalScoreValue = PlayerPrefs.GetInt("totalScore");

        if (totalScoreValue < 0)
        {
            totalScoreValue = 0;
        }

        totalScoreText.text = "Total Score: " + totalScoreValue;
    }

    IEnumerator Confetti(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        VFX.Play();
    }

    // Method to reset deeds and scores
    private void ResetDeedsAndScores()
    {
        for (int i = 0; i < deedTexts.Length; i++)
        {
            PlayerPrefs.SetFloat("Deeds_" + (i + 1), 0);
            PlayerPrefs.SetFloat("Scores_" + (i + 1), 0);
        }
    }

    // Method to handle retry button click
    public void OnRetryButtonClick()
    {
        ResetDeedsAndScores();
        SceneManager.LoadScene("Level");
    }

    // Method to handle back to main menu button click
    public void OnBackToMainMenuButtonClick()
    {
        ResetDeedsAndScores();
        SceneManager.LoadScene("MainMenu");
    }
}