using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class timeController : MonoBehaviour
{
    private AudioManager audioManager;

    private AdmobAdsScript admobAdsScript;

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

    private bool audioPlayed = false; // New variable to track audio playing status

    private void Awake()
    {
        admobAdsScript = FindObjectOfType<AdmobAdsScript>();
        // Find the questionSetup component in the scene
        questionSetup = FindObjectOfType<questionSetup>();
        if (questionSetup == null)
        {
            Debug.LogError("questionSetup not found in the scene!");
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Start()
    {
        timeRemaining = PlayerPrefs.GetFloat("Timer", maxTime); // Load the timer value from PlayerPrefs
        admobAdsScript.LoadInterstitialAd();
        ResetTimer();
    }

    // Method to reset the timer to its initial value
    public void ResetTimer()
    {
        timeRemaining = maxTime;
        audioPlayed = false;
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Level Type") == "QuizSurahSelect")
        {
            fillBar.gameObject.SetActive(true);
            practiceText.gameObject.SetActive(false);

            if (timeRemaining > 0)
            {
                if (!audioPlayed) // Check if audio has not been played yet
                {
                    audioManager.PlaySFX(audioManager.changePageButtonSFX);
                    audioPlayed = true; // Set the flag to true to indicate audio has been played
                }

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
                    StartCoroutine(resultPage(1));
                }
            }
        }
        else
        {
            fillBar.gameObject.SetActive(false);
            practiceText.gameObject.SetActive(true);
        }
    }

    // Method to add time to the timer
    public void AddTime(float additionalTime)
    {
        timeRemaining += additionalTime;
    }

    IEnumerator resultPage(int waitTime)
    {
        admobAdsScript.ShowInterstitialAd();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Result");
    }
}
