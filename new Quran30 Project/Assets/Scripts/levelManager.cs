using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public Image expFillBar;

    [SerializeField]
    private TextMeshProUGUI playerLevel;
    [SerializeField]
    private TextMeshProUGUI expPoints;
    [SerializeField]
    private TextMeshProUGUI expPercentage;
    [SerializeField]
    private TextMeshProUGUI playerRank;
    [SerializeField]
    private TextMeshProUGUI nextRank;

    private void Start()
    {
        StartCoroutine(ExpIncreaseCoroutine()); //UNCOMMENT TO TEST LEVEL
    }

    private IEnumerator ExpIncreaseCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            PlayerPrefs.SetInt("currentExp", PlayerPrefs.GetInt("currentExp") + 1);
        }
    }

    private void UpdateUI()
    {
        int currentLevel = PlayerPrefs.GetInt("playerLevel");
        playerLevel.text = currentLevel.ToString();
        expPoints.text = PlayerPrefs.GetInt("currentExp") + "/" + PlayerPrefs.GetInt("currentMaxExp");

        float fillAmount = (float)PlayerPrefs.GetInt("currentExp") / PlayerPrefs.GetInt("currentMaxExp");
        expFillBar.fillAmount = fillAmount;

        // Calculate percentage and display it
        int currentExp = PlayerPrefs.GetInt("currentExp");
        int maxExp = PlayerPrefs.GetInt("currentMaxExp");
        float percentage = (float)currentExp / maxExp * 100;
        expPercentage.text = percentage.ToString("F0") + "%";

        // Set player's current and next rank
        GetPlayerRanks(currentLevel, out string currentRank, out string nextRankText);
        playerRank.text = currentRank;
        nextRank.text = nextRankText;
    }

    private void GetPlayerRanks(int level, out string currentRank, out string nextRank)
    {
        currentRank = "";
        nextRank = "";

        int rankIndex = level / 10;
        int rankLevel = level % 10;

        switch (rankIndex)
        {
            case 0:
                currentRank = "Novice " + RomanNumeral(rankLevel);
                nextRank = "Novice " + RomanNumeral(rankLevel + 1);
                break;
            case 1:
                currentRank = "Amateur " + RomanNumeral(rankLevel);
                nextRank = "Amateur " + RomanNumeral(rankLevel + 1);
                break;
            case 2:
                currentRank = "Learner " + RomanNumeral(rankLevel);
                nextRank = "Learner " + RomanNumeral(rankLevel + 1);
                break;
            case 3:
                currentRank = "Scholar " + RomanNumeral(rankLevel);
                nextRank = "Scholar " + RomanNumeral(rankLevel + 1);
                break;
            case 4:
                currentRank = "Insider " + RomanNumeral(rankLevel);
                nextRank = "Insider " + RomanNumeral(rankLevel + 1);
                break;
            case 5:
                currentRank = "Expert " + RomanNumeral(rankLevel);
                nextRank = "Expert " + RomanNumeral(rankLevel + 1);
                break;
            case 6:
                currentRank = "Master " + RomanNumeral(rankLevel);
                nextRank = "Master " + RomanNumeral(rankLevel + 1);
                break;
            case 7:
                currentRank = "Grandmaster " + RomanNumeral(rankLevel);
                nextRank = "Grandmaster " + RomanNumeral(rankLevel + 1);
                break;
            case 8:
                currentRank = "Quiz Virtuoso " + RomanNumeral(rankLevel);
                nextRank = "Quiz Virtuoso " + RomanNumeral(rankLevel + 1);
                break;

            default:
                currentRank = "MAX RANK";
                nextRank = "MAX RANK"; // Example for levels beyond 100
                break;
        }
    }

    private string RomanNumeral(int number)
    {
        if (number < 1 || number > 10)
        {
            return "";
        }
        else
        {
            switch (number)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                case 6:
                    return "VI";
                case 7:
                    return "VII";
                case 8:
                    return "VIII";
                case 9:
                    return "IX";
                case 10:
                    return "X";
                default:
                    return "";
            }
        }
    }



    private void Update()
    {
        IncreaseMaxExp(); // First update the player's exp and level
        UpdateUI();       // Then update the UI to reflect the changes

    }

    public void IncreaseMaxExp()
    {
        if (PlayerPrefs.GetInt("currentExp") >= PlayerPrefs.GetInt("currentMaxExp"))
        {
            PlayerPrefs.SetInt("currentMaxExp", PlayerPrefs.GetInt("currentMaxExp") + 50);
            PlayerPrefs.SetInt("currentExp", 0);
            PlayerPrefs.SetInt("playerLevel", PlayerPrefs.GetInt("playerLevel") + 1);
        }
    }
}