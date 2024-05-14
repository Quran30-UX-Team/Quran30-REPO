using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class practiceSelect : MonoBehaviour
{
    public AudioManager audioManager;

    public Button button;

    public GameObject Button1;
    public GameObject descPanel1;

    public GameObject Button2;
    public GameObject descPanel2;

    public RectTransform Button2Transform;
    public RectTransform descPanel2Transform;

    static int BUTTON_DISTANCE = 120;

    public bool isDrop;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        button.interactable = false;
        Button2Transform = Button2.GetComponent<RectTransform>();
        descPanel2Transform = descPanel2.GetComponent<RectTransform>();
    }

    public void descToggle()
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);

        if (isDrop == false)
        {
            descPanel1.SetActive(true);
            button.interactable = true;
            PlayerPrefs.SetString("Level Type", "PracticeSurahSelect");
            Button2Transform.anchoredPosition -= new Vector2(0, BUTTON_DISTANCE);
            descPanel2Transform.anchoredPosition -= new Vector2(0, BUTTON_DISTANCE);
            isDrop = true;
            descPanel2.SetActive(false);
        }

        else

        {
            descPanel1.SetActive(false);
            button.interactable = false;
            Button2Transform.anchoredPosition += new Vector2(0, BUTTON_DISTANCE);
            descPanel2Transform.anchoredPosition += new Vector2(0, BUTTON_DISTANCE);
            isDrop = false;
        }
    }

    public void quizDesc()
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);

        if (isDrop == true)
        {
            descPanel1.SetActive(false);
            Button2Transform.anchoredPosition += new Vector2(0, BUTTON_DISTANCE);
            descPanel2Transform.anchoredPosition += new Vector2(0, BUTTON_DISTANCE);
            isDrop = false;

            descPanel2.SetActive(true);
            button.interactable = true;
            PlayerPrefs.SetString("Level Type", "QuizSurahSelect");
        }

        else
        {
            if (descPanel2.activeSelf == false)
            {
                descPanel2.SetActive(true);
                button.interactable= true;
                PlayerPrefs.SetString("Level Type", "QuizSurahSelect");
            }
            else
            {
                descPanel2.SetActive(false);
                button.interactable= false;
            }
        }

    }
}
