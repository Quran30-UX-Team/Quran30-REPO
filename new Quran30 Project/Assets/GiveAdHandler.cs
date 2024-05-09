using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveAdHandler : MonoBehaviour
{
    public int giveAdEvery;
    public int chanceOfADType;

    [SerializeField] private GameObject SkippableAdPanel;
    [SerializeField] private TextMeshProUGUI skippableAdCountdownText;
    [SerializeField] private GameObject UnSkippableAdPanel;
    [SerializeField] private TextMeshProUGUI unskippableAdCountdownText;

    private void Start()
    {
        PlayerPrefs.SetInt("GiveAdCounter", PlayerPrefs.GetInt("GiveAdCounter") + 1);

        Debug.Log(PlayerPrefs.GetInt("GiveAdCounter"));

        if (PlayerPrefs.GetInt("GiveAdCounter") == giveAdEvery)
        {
            AdTypeProbability();
            PlayerPrefs.SetInt("GiveAdCounter", 0);
        }
    }

    private void AdTypeProbability()
    {
        int randomNumber = Random.Range(1, chanceOfADType + 1);

        Debug.Log(randomNumber);

        if (randomNumber == chanceOfADType)
        {
            StartCoroutine(UnSkippableAd(30));
        }
        else
        {
            StartCoroutine(SkippableAd(10));
        }
    }

    IEnumerator SkippableAd(int seconds)
    {
        SkippableAdPanel.SetActive(true);
        skippableAdCountdownText.text = seconds.ToString();

        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
            skippableAdCountdownText.text = seconds.ToString();
        }

        SkippableAdPanel.SetActive(false);
    }

    IEnumerator UnSkippableAd(int seconds)
    {
        UnSkippableAdPanel.SetActive(true);
        unskippableAdCountdownText.text = seconds.ToString();

        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
            unskippableAdCountdownText.text = seconds.ToString();
        }

        UnSkippableAdPanel.SetActive(false);
    }
}
