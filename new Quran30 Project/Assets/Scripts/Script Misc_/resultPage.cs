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

    public TextMeshProUGUI congratsPanel;
    public TextMeshProUGUI rewardPanel;

    private TextMeshProUGUI congratsPanelText;
    private TextMeshProUGUI rewardPanelText;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public TextMeshProUGUI Title;

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
        congratsPanelText = congratsPanel.GetComponent<TextMeshProUGUI>();
        rewardPanelText = rewardPanel.GetComponent<TextMeshProUGUI>();

        float totalDeeds = 0;
        float totalScores = 0;

        if (DevBtn.devMode != false)
        {
            devPanel.SetActive(true);
        }

        // Set deed and score texts dynamically
        for (int i = 0; i < deedTexts.Length; i++)
        {
            deedTexts[i].text = "Q" + (i + 1) + " Deeds: " + PlayerPrefs.GetFloat("Deeds_" + (i + 1));
            scoreTexts[i].text = "Q" + (i + 1) + " Scores: " + PlayerPrefs.GetFloat("Scores_" + (i + 1));

            totalDeeds += PlayerPrefs.GetFloat("Deeds_" + (i + 1));
            totalScores += PlayerPrefs.GetFloat("Scores_" + (i + 1));

            // Debug log to show the calculation of total deeds
            Debug.Log("Deeds for Question " + (i + 1) + ": " + PlayerPrefs.GetFloat("Deeds_" + (i + 1)));
        }

        // Double the total deeds for RushSurahSelect mode
        if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect")
        {
            totalDeeds *= 2;
        }

        totalDeed.text = "Total Deeds: " + totalDeeds.ToString();
        totalScore.text = "Total Scores: " + totalScores.ToString();

        // Check the score and activate stars accordingly
        if (totalScores < 0.04)
        {
            Star1.SetActive(false);
            Star2.SetActive(false);
            Star3.SetActive(false);
            Title.text = "TRY AGAIN!";
        }
        else if (totalScores < 15)
        {
            Star1.SetActive(true);
            Star2.SetActive(false);
            Star3.SetActive(false);
            Title.text = "CONGRATULATION!";

            audioManager.PlaySFX(audioManager.resultPageSFX);
        }
        else if (totalScores < 30)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(false);
            Title.text = "CONGRATULATION!";

            audioManager.PlaySFX(audioManager.resultPageSFX);
        }
        else
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
            Title.text = "CONGRATULATION!";

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
        rewardPanelText.text = "RECEIVED " + "<b>" + roundedDeed + "</b>" + " DEEDS";

        PlayerPrefs.SetFloat("Deeds", totalDeeds);
        PlayerPrefs.SetFloat("Score", 0);

        if (totalDeeds > 0)
        {
            PlayerPrefs.SetInt("totalDeed", PlayerPrefs.GetInt("totalDeed") + (int)totalDeeds);
        }

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
}