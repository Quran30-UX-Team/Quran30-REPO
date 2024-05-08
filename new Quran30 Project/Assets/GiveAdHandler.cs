using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class GiveAdHandler : MonoBehaviour
{
    public int giveAdEvery;
    public int randomRange;

    [SerializeField] private GameObject SkippableAdPanel;
    [SerializeField] private GameObject UnSkippableAdPanel;

    private void Start()
    {
        PlayerPrefs.SetInt("GiveAdCounter", PlayerPrefs.GetInt("GiveAdCounter") + 1);

        print(PlayerPrefs.GetInt("GiveAdCounter"));

        if (PlayerPrefs.GetInt("GiveAdCounter") == giveAdEvery)
        {
            AdTypeProbability();
            PlayerPrefs.SetInt("GiveAdCounter", 0);
        }
    }

    private void AdTypeProbability()
    {
        int randomNumber = Random.Range(1, randomRange + 1);

        Debug.Log(randomNumber);

        if (randomNumber == (randomRange))
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
        yield return new WaitForSeconds(seconds);
        SkippableAdPanel.SetActive(false);
    }

    IEnumerator UnSkippableAd(int seconds)
    {
        UnSkippableAdPanel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        UnSkippableAdPanel.SetActive(false);
    }
}
