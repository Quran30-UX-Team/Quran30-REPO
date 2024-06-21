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

    public GameObject Button3;
    public GameObject descPanel3;

    public RectTransform Button2Transform;
    public RectTransform descPanel2Transform;

    public RectTransform Button3Transform;
    public RectTransform descPanel3Transform;

    private Vector2 originalButton1Position;
    private Vector2 originalButton2Position;
    private Vector2 originalButton3Position;

    private void Start()
    {
        button.interactable = false;
        originalButton1Position = Button1.GetComponent<RectTransform>().anchoredPosition;
        originalButton2Position = Button2.GetComponent<RectTransform>().anchoredPosition;
        originalButton3Position = Button3.GetComponent<RectTransform>().anchoredPosition;
    }

    public void descToggle()
    {
        DeactivateAllPanels();
        if (!descPanel1.activeSelf)
        {
            ActivatePanel(descPanel1, originalButton2Position, originalButton3Position, 106);
            PlayerPrefs.SetString("Level Type", "PracticeSurahSelect");
        }
    }

    public void quizDesc()
    {
        DeactivateAllPanels();
        if (!descPanel2.activeSelf)
        {
            ActivatePanel(descPanel2, originalButton3Position, originalButton1Position, 106);
            PlayerPrefs.SetString("Level Type", "QuizSurahSelect");
        }
    }

    public void rushHourDesc()
    {
        DeactivateAllPanels();
        if (!descPanel3.activeSelf)
        {
            ActivatePanel(descPanel3, Vector2.zero, Vector2.zero, 2);
            PlayerPrefs.SetString("Level Type", "RushSurahSelect");
        }
    }

    void DeactivateAllPanels()
    {
        descPanel1.SetActive(false);
        descPanel2.SetActive(false);
        descPanel3.SetActive(false);

        // Reset button positions
        Button1.GetComponent<RectTransform>().anchoredPosition = originalButton1Position;
        Button2.GetComponent<RectTransform>().anchoredPosition = originalButton2Position;
        Button3.GetComponent<RectTransform>().anchoredPosition = originalButton3Position;
    }

    void ActivatePanel(GameObject panel, Vector2 button2Position, Vector2 button3Position, float yOffset)
    {
        panel.SetActive(true);
        button.interactable = true;

        if (button2Position != Vector2.zero && panel == descPanel1)
        {
            Button2.GetComponent<RectTransform>().anchoredPosition = button2Position - new Vector2(0, yOffset);
        }
        if (button3Position != Vector2.zero)
        {
            Button3.GetComponent<RectTransform>().anchoredPosition = originalButton3Position - new Vector2(0, yOffset);
        }
    }
}