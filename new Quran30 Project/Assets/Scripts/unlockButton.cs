using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class unlockButton : MonoBehaviour
{
    public GameObject unlockPanel;

    public GameObject lockCode; // The lock number associated with this button
    public GameObject unlockBtn;
    public GameObject panel;

    // Use a key prefix to distinguish between different locks in PlayerPrefs
    private string unlockKey;

    private void Awake()
    {
        unlockKey = "LockUnlocked_" + lockCode.name;
        StartCoroutine(findPanel());
    }
    private void Start()
    {
        // Check if this lock is already unlocked and adjust button visibility accordingly
        if (PlayerPrefs.GetInt(unlockKey, 0) == 1)
        {
            unlockBtn.SetActive(false);
            panel.SetActive(false);
        }
    }

    public void openUnlockPanel()
    {
        unlockPanel.SetActive(true);
        panel.SetActive(false);
        PlayerPrefs.SetString("currentLock", unlockKey);
        PlayerPrefs.SetString("currentButton", lockCode.name);
        PlayerPrefs.Save();

        //if (PlayerPrefs.GetFloat("Deeds") > 19)
        //{
        //    PlayerPrefs.SetFloat("Deeds", PlayerPrefs.GetFloat("Deeds") - 20);
        //    // Set the lock as unlocked
        //    PlayerPrefs.SetInt(unlockKey, 1);
        //    unlockBtn.SetActive(false);
        //    panel.SetActive(false);
        //}
    }

    IEnumerator findPanel()
    {
        unlockPanel = GameObject.Find("unlockPanel");
        yield return new WaitForSeconds(0.1f);
        unlockPanel.SetActive(false);
    }
}
