using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class levelButton : MonoBehaviour
{
    public GameObject description;
    public GameObject lockImage;

    public TextMeshProUGUI buttonText;
    public string surahName;
    public string surahQuestionSet;

    public bool isShow = false;

    // Reference to the container holding all level buttons
    public Transform buttonContainer;

    private void Start()
    {
        StartCoroutine(autoHide());
    }

    public void interactButton(string sceneName)
    {
        if (lockImage.activeSelf == true)
        {
            Debug.Log("Button is LOCKED");
            controlDesc();
        }
        else
        {
            PlayerPrefs.SetString("Question Set", surahQuestionSet);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void controlDesc()
    {
        if (isShow == false)
        {
            showDesc();
            // Collapse other descriptions
            CollapseOtherDescriptions();
            isShow = true;
        }
        else
        {
            hideDesc();
            isShow = false;
        }
    }

    public void showDesc()
    {
        description.SetActive(true);
        isShow = true;
    }

    public void hideDesc()
    {
        description.SetActive(false);
        isShow = false;
    }

    IEnumerator autoHide()
    {
        yield return new WaitForSeconds(0.01f);
        buttonText.text = surahName;
        description.SetActive(false);
        isShow = false;
    }

    // Helper method to collapse descriptions of other buttons
    private void CollapseOtherDescriptions()
    {
        foreach (Transform button in buttonContainer)
        {
            if (button != transform) // Skip current button
            {
                var buttonScript = button.GetComponent<levelButton>();
                if (buttonScript != null && buttonScript.isShow)
                {
                    buttonScript.hideDesc();
                }
            }
        }
    }
}
