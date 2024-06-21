using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
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
    private bool timerRunning = false; // New variable to track timer status

    public float maxTime;
    public float maxRushTime;
    public float currTime;

    private bool audioPlayed = false; // New variable to track audio playing status

    public GameObject blockPanel;

    private bool isTransitioning = false; // New variable to track transition state

    private void Awake()
    {
        // Find the questionSetup component in the scene
        questionSetup = FindObjectOfType<questionSetup>();
        if (questionSetup == null)
        {
            Debug.LogError("questionSetup not found in the scene!");
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        // Activate blockPanel
        blockPanel.SetActive(true);
    }

    public void Start()
    {
        timeRemaining = PlayerPrefs.GetFloat("Timer", maxTime); // Load the timer value from PlayerPrefs
        ResetTimer();

        // Ensure PUBlockPanel is active if in RushSurahSelect mode
        if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect")
        {
            blockPanel.SetActive(true);
        }
    }

    // Method to reset the timer to its initial value
    public void ResetTimer()
    {
        if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect")
        {
            timeRemaining = maxRushTime;
        }
        else
        {
            timeRemaining = maxTime;
        }
        audioPlayed = false;
        timerRunning = true; // Start the timer
    }

    // Method to stop the timer
    public void StopTimer()
    {
        timerRunning = false;
    }

    // Method to start the timer
    public void StartTimer()
    {
        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning && (PlayerPrefs.GetString("Level Type") == "QuizSurahSelect" || PlayerPrefs.GetString("Level Type") == "RushSurahSelect"))
        {
            fillBar.gameObject.SetActive(true);
            practiceText.gameObject.SetActive(false);

            if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect" && !blockPanel.activeSelf)
            {
                blockPanel.SetActive(true);
            }

            if (timeRemaining > 0)
            {
                if (!audioPlayed) // Check if audio has not been played yet
                {
                    audioManager.PlaySFX(audioManager.changePageButtonSFX);
                    audioPlayed = true; // Set the flag to true to indicate audio has been played
                }

                PlayerPrefs.SetFloat("Timer", timeRemaining);
                timeRemaining -= Time.deltaTime;
                fillBar.fillAmount = timeRemaining / (PlayerPrefs.GetString("Level Type") == "RushSurahSelect" ? maxRushTime : maxTime);

                // Calculate the normalized position along the gradient
                float normalizedTime = 1 - (timeRemaining / (PlayerPrefs.GetString("Level Type") == "RushSurahSelect" ? maxRushTime : maxTime));

                // Get the color corresponding to the normalized position from the gradient
                Color targetColor = _barGradient.Evaluate(normalizedTime);

                // Lerp between the current color and the target color
                fillBar.color = Color.Lerp(fillBar.color, targetColor, Time.deltaTime * 5f); // You can adjust the speed by changing the last parameter
            }
            else
            {
                if (!isTransitioning)
                {
                    HandleTimeOut();
                }
            }
        }
        else if (PlayerPrefs.GetString("Level Type") == "PracticeSurahSelect")
        {
            fillBar.gameObject.SetActive(false);
            practiceText.gameObject.SetActive(true);
        }
    }

    private void HandleTimeOut()
    {
        isTransitioning = true; // Set the transition state to true

        // Check if the current level type is RushSurahSelect
        if (PlayerPrefs.GetString("Level Type") == "RushSurahSelect")
        {
            // Reset deeds and scores to 0 for remaining questions
            for (int i = questionSetup.counter; i < questionSetup.questions.Count; i++)
            {
                PlayerPrefs.SetFloat("Deeds_" + i, 0);
                PlayerPrefs.SetFloat("Scores_" + i, 0);
            }

            // Transition to the Result scene immediately
            StartCoroutine(resultPage(0));
        }
        else
        {
            // Check if there are remaining questions
            if (questionSetup.questions.Count > 0)
            {
                // Reset the timer when time runs out
                ResetTimer();

                // Set deeds and scores to 0 for the current question
                int i = questionSetup.counter;
                PlayerPrefs.SetFloat("Deeds_" + i, 0);
                PlayerPrefs.SetFloat("Scores_" + i, 0);

                // Move to the next question
                questionSetup.Start();
                doublePanel.SetActive(false);
                Debug.Log("RAN OUT OF TIME");

                // Debug log wrong deeds and scores
                Debug.Log("Wrong Deeds: " + PlayerPrefs.GetFloat("Deeds_" + i));
                Debug.Log("Wrong Scores: " + PlayerPrefs.GetFloat("Scores_" + i));
            }
            else
            {
                // Transition to the result page after a short delay
                StartCoroutine(resultPage(1));
            }
        }

        isTransitioning = false; // Reset the transition state
    }

    // Method to add time to the timer
    public void AddTime(float additionalTime)
    {
        timeRemaining += additionalTime;
    }

    IEnumerator resultPage(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Result");
    }
}