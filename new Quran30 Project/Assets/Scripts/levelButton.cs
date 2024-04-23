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
}
