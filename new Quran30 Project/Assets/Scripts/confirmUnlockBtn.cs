using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmUnlockBtn : MonoBehaviour
{
    private string levelBtn;
    private string Lock;
    public GameObject unlockPanel;
    public void confirmUnlock()
    {
        PlayerPrefs.SetInt(PlayerPrefs.GetString("currentLock"), 1);

        // Find the GameObject with the name stored in levelBtnName
        GameObject levelBtn = GameObject.Find(PlayerPrefs.GetString("currentButton"));

        if (levelBtn != null)
        {
            // Find the child GameObject named "Button" under levelBtn
            Transform buttonTransform = levelBtn.transform.Find("Button");

            if (buttonTransform != null)
            {
                // Find the child GameObject named "Lock" under Button
                Transform lockTransform = buttonTransform.Find("Lock");

                if (lockTransform != null)
                {
                    // Get the GameObject reference for the Lock
                    GameObject lockObject = lockTransform.gameObject;

                    lockObject.SetActive(false);
                    PlayerPrefs.SetInt(PlayerPrefs.GetString("currentLock"), 1);
                    unlockPanel.SetActive(false);

                    if (PlayerPrefs.GetFloat("Deeds") > 19)
                    {
                        PlayerPrefs.SetFloat("Deeds", PlayerPrefs.GetFloat("Deeds") - 20);
                        lockObject.SetActive(false);
                        PlayerPrefs.SetInt(PlayerPrefs.GetString("currentLock"), 1);
                        unlockPanel.SetActive(false);
                    }

                }
                else
                {
                    Debug.LogWarning("Child object named 'Lock' not found under Button");
                }
            }
            else
            {
                Debug.LogWarning("Child object named 'Button' not found under levelBtn");
            }
        }
        else
        {
            Debug.LogWarning("GameObject named '" + PlayerPrefs.GetString("currentButton") + "' not found");
        }

    }

}
