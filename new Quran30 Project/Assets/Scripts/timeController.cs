using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;
using System;

public class timeController : MonoBehaviour
{
    public GameObject doublePanel;

    private questionSetup questionSetup;

    [SerializeField]
    private Gradient _barGradient;

    [SerializeField]
    private TextMeshProUGUI practiceText;

    public Image fillBar;
    float timeRemaining;

    public float maxTime;
    public float currTime;

    private void Awake()
    {
        // Find the questionSetup component in the scene
        questionSetup = FindObjectOfType<questionSetup>();
        if (questionSetup == null)
        {
            Debug.LogError("questionSetup not found in the scene!");
        }
    }

    public void Start()
    {
        timeRemaining = maxTime;
        PlayerPrefs.SetFloat("Timer", maxTime);
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Level Type") == "QuizSurahSelect")
        {
            fillBar.gameObject.SetActive(true);
            practiceText.gameObject.SetActive(false);

            if (timeRemaining > 0)
            {
                PlayerPrefs.SetFloat("Timer", timeRemaining);
                timeRemaining -= Time.deltaTime;
                fillBar.fillAmount = timeRemaining / maxTime;

                // Calculate the normalized position along the gradient
                float normalizedTime = 1 - (timeRemaining / maxTime);

                // Get the color corresponding to the normalized position from the gradient
                Color targetColor = _barGradient.Evaluate(normalizedTime);

                // Lerp between the current color and the target color
                fillBar.color = Color.Lerp(fillBar.color, targetColor, Time.deltaTime * 5f); // You can adjust the speed by changing the last parameter
            }
            else
            {
                if (questionSetup.questions.Count > 0)
                {
                    questionSetup.Start();
                    Start();
                    doublePanel.SetActive(false);
                    Debug.Log("RAN OUT OF TIME");
                }

                if (questionSetup.questions.Count == 0)
                {
                    SceneManager.LoadScene("Result");
                }
            }
        }

        else
        {
            fillBar.gameObject.SetActive(false);
            practiceText.gameObject.SetActive(true);
        }
        
    }

}
