using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resultPage : MonoBehaviour
{
    public ParticleSystem particleSystem;

    [SerializeField]
    private float deeds;

    public TextMeshProUGUI congratsPanel;
    public TextMeshProUGUI rewardPanel;

    private TextMeshProUGUI congratsPanelText;
    private TextMeshProUGUI rewardPanelText;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public TextMeshProUGUI[] deedTexts;
    public TextMeshProUGUI[] scoreTexts;

    public TextMeshProUGUI totalDeed;
    public TextMeshProUGUI totalScore;

    public GameObject devPanel;

    private void Awake()
    {
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
    }

    public void Start()
    {
        particleSystem.Play();

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
        }

        totalDeed.text = "Total Deeds: " + totalDeeds.ToString();
        totalScore.text = "Total Scores: " + totalScores.ToString();

        // Check the score and activate stars accordingly
        if (totalScores >= 30)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
        else if (totalScores >= 15)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(false);
        }
        else if (totalScores >= 0.04)
        {
            Star1.SetActive(true);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }
        else
        {
            Star1.SetActive(false);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }

        string roundedScore = Mathf.RoundToInt(totalScores).ToString();
        string roundedDeed = Mathf.RoundToInt(totalDeeds).ToString();

        if (PlayerPrefs.GetString("Level Type") == "PracticeSurahSelect")
        {
            totalDeeds = 0;
            roundedDeed = "0";
        }

        deeds = PlayerPrefs.GetFloat("Deeds") + totalDeeds;

        congratsPanelText.text = "SCORES: " + "<b>" + roundedScore + "</b>";
        rewardPanelText.text = "RECEIVED " + "<b>" + roundedDeed + "</b>" + " DEEDS";

        PlayerPrefs.SetFloat("Deeds", deeds);
        PlayerPrefs.SetFloat("Score", 0);
    }
}
