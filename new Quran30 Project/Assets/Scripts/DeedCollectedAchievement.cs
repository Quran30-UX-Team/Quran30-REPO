using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeedCollectedAchievement : MonoBehaviour
{
    private AudioManager audioManager;

    public string achievementTitle;
    public string achievementDesc;

    public int totalDeedsToAchieve;
    public TextMeshProUGUI achievementTitleText;
    public TextMeshProUGUI achievementDescText;

    public GameObject progressContainer;
    public Image progressFillbar;
    public GameObject Badge;

    public GameObject acceptPanel;

    private bool isCompleted = false; // Track if achievement is completed

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        achievementTitleText.text = achievementTitle;
        achievementDescText.text = achievementDesc;

        // Check if achievement was already completed and accepted
        if (PlayerPrefs.GetInt("Achievement_" + achievementTitle + "_Completed") == 1)
        {
            isCompleted = true;
            COMPLETED();

            // Check if achievement was accepted
            if (PlayerPrefs.GetInt("Achievement_" + achievementTitle + "_Accepted") == 1)
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
            PlayerPrefs.SetInt("Achievement_" + achievementTitle + "_Completed", 1); // Save completion state
        }
    }

    public void COMPLETED()
    {
        progressContainer.SetActive(false);
        this.gameObject.tag = "CompletedAchievement";
    }

    public void acceptAchievement()
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        acceptPanel.SetActive(false);
        Badge.SetActive(true);
        PlayerPrefs.SetInt("Achievement_" + achievementTitle + "_Accepted", 1); // Save acceptance state
        this.gameObject.tag = "Claimed";
    }
}
