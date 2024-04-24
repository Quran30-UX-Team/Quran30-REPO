using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementPanelHandler : MonoBehaviour
{
    public string achievementName;
    public int achievementLevel;

    private int achievementMaxExp;
    public TextMeshProUGUI achievementNameText;

    public GameObject progressContainer;
    public Image progressFillbar;

    private void Start()
    {
        achievementNameText.text = achievementName;
        CalculateMaxExp();
    }

    private void Update()
    {
        int currentLevel = PlayerPrefs.GetInt("playerLevel");

        float fillAmount = (float)PlayerPrefs.GetInt("currentExp") / achievementMaxExp;
        progressFillbar.fillAmount = fillAmount;

        if (currentLevel == achievementLevel)
        {
            progressContainer.SetActive(false);
        }
    }

    private void CalculateMaxExp()
    {
        achievementMaxExp = 50 * (achievementLevel + 0); // Ensure achievementMaxExp is at least 1
    }
}