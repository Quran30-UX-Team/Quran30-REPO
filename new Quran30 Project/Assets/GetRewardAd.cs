using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetRewardAd : MonoBehaviour
{
    [SerializeField] private GameObject UnSkippableAdPanel;
    [SerializeField] private TextMeshProUGUI unskippableAdCountdownText;
    public void onClickAD()
    {
        StartCoroutine(SkippableAd(30));
    }

    IEnumerator SkippableAd(int seconds)
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
        PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") + 30);
    }
}
