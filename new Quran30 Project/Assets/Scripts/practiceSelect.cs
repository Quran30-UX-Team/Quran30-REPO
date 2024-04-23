using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class practiceSelect : MonoBehaviour
{
    public Button button;

    public GameObject Button1;
    public GameObject descPanel1;

    public GameObject Button2;
    public GameObject descPanel2;

    public RectTransform Button2Transform;
    public RectTransform descPanel2Transform;

    public bool isDrop;

    private void Start()
    {
        button.interactable = false;
        Button2Transform = Button2.GetComponent<RectTransform>();
        descPanel2Transform = descPanel2.GetComponent<RectTransform>();
    }

    public void descToggle()
    {
        if (isDrop == false)
        {
            descPanel1.SetActive(true);
            button.interactable = true;
            PlayerPrefs.SetString("Level Type", "PracticeSurahSelect");
            Button2Transform.anchoredPosition -= new Vector2(0, 100);
            descPanel2Transform.anchoredPosition -= new Vector2(0, 100);
            isDrop = true;
            descPanel2.SetActive(false);
        }

        else

        {
            descPanel1.SetActive(false);
            button.interactable = false;
            Button2Transform.anchoredPosition += new Vector2(0, 100);
            descPanel2Transform.anchoredPosition += new Vector2(0, 100);
            isDrop = false;
        }
    }

    public void quizDesc()
    {
        if (isDrop == true)
        {
            descPanel1.SetActive(false);
            Button2Transform.anchoredPosition += new Vector2(0, 100);
            descPanel2Transform.anchoredPosition += new Vector2(0, 100);
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
