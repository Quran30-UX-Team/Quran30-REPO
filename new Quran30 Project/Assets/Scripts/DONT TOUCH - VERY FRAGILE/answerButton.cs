using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class answerButton : MonoBehaviour
{
    public GameObject doublePanel;
    public GameObject blockPanel;

    private AdmobAdsScript admobAdsScript;

    public GameObject pickColor;

    public AudioManager audioManager;

    public doubleDeeds doubleDeeds;

    private bool isCorrect;
    private TextMeshProUGUI answerText;

    private questionSetup questionSetup;
    private timeController timeController;

    [SerializeField] private Color correctColor1;
    [SerializeField] private Color correctColor2;

    [SerializeField] private Color wrongColor1;
    [SerializeField] private Color wrongColor2;

    public Image buttonP1;
    public Image buttonP2;

    private Color defaultColor1;
    private Color defaultColor2;

    [SerializeField]
    private int score;

    private float rightDeeds;
    private float rightScores;

    private float wrongDeeds;
    private float wrongScores;

    private void Awake()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        answerText = GetComponentInChildren<TextMeshProUGUI>();

        // Find the questionSetup component in the scene
        questionSetup = FindObjectOfType<questionSetup>();
        if (questionSetup == null)
        {
            Debug.LogError("questionSetup not found in the scene!");
        }

        timeController = FindObjectOfType<timeController>();
        if (timeController == null)
        {
            Debug.LogError("timeController not found in the scene!");
        }

        admobAdsScript = FindObjectOfType<AdmobAdsScript>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        blockPanel = GameObject.Find("blockPanel");
    }

    private void Start()
    {
        admobAdsScript.LoadInterstitialAd();
        defaultColor1 = buttonP1.color;
    }

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void onClick()
    {
        // Stop the timer when an answer button is clicked //baiki sini utk set wrong answer lead ke page reward
        timeController.StopTimer();

        StartCoroutine(debounce(isCorrect ? 1 : 2));

        int i = questionSetup.counter;
        doublePanel.SetActive(false);

        if (isCorrect)
        {
            audioManager.PlaySFX(audioManager.correctAnswerSFX);
            // Calculate deeds and scores for the current question
            rightDeeds = PlayerPrefs.GetFloat("Timer") * 0.5f;
            rightScores = PlayerPrefs.GetFloat("Timer") * 0.5f;

            Debug.Log("Right Timer: " + PlayerPrefs.GetFloat("Timer"));
            Debug.Log("Right Deeds: " + rightDeeds);
            Debug.Log("Right Scores: " + rightScores);

            // Save deeds and scores for the current question
            PlayerPrefs.SetFloat("Deeds_" + i, Mathf.Round(rightDeeds * 100f) / 100f);
            PlayerPrefs.SetFloat("Scores_" + i, Mathf.Round(rightScores * 100f) / 100f);

            // Check if the power-up is activated and apply it only once per question
            if (PlayerPrefs.GetInt("DoubleDeedPU") == 1)
            {
                DoubleDeedsPowerUp();
            }

            // Change the button's color to green
            StartCoroutine(turnGreen(0.75f));
        }
        else
        {
            audioManager.PlaySFX(audioManager.wrongAnswerSFX);

            wrongDeeds = PlayerPrefs.GetFloat("Timer") * 0.05f;
            wrongScores = (11f - PlayerPrefs.GetFloat("Timer")) * -0.75f;

            Debug.Log("Wrong Timer: " + PlayerPrefs.GetFloat("Timer"));
            Debug.Log("Wrong Deeds: " + wrongDeeds);
            Debug.Log("Wrong Scores: " + wrongScores);

            // Save deeds and scores for the current question
            PlayerPrefs.SetFloat("Deeds_" + i, Mathf.Round(wrongDeeds * 100f) / 100f);
            PlayerPrefs.SetFloat("Scores_" + i, Mathf.Round(wrongScores * 100f) / 100f);

            // Change the button's color to red
            StartCoroutine(turnRed(0.75f));

            if (PlayerPrefs.GetString("Level Type") != "QuizSurahSelect")
            {
                // Find the correct answer button and turn it green
                answerButton[] answerButtons = FindObjectsOfType<answerButton>();
                answerButton correctButton = answerButtons.First(button => button.isCorrect);
                correctButton.StartCoroutine(correctButton.turnGreen(2f));
            }
        }

        if (questionSetup.questions.Count > 0)
        {
            if (PlayerPrefs.GetString("Level Type") == "QuizSurahSelect")
            {
                StartCoroutine(reload(1));
            }
            else
            {
                StartCoroutine(reload(isCorrect ? 1 : 2)); // Shorter delay for correct, longer for wrong answers
            }
        }
        else
        {
            // If there is only one question left, change the scene
            int sceneEntriesCount = PlayerPrefs.GetInt("AdSceneEntriesCount");
            sceneEntriesCount += 1;
            Debug.Log("Entry: " + sceneEntriesCount);
            PlayerPrefs.SetInt("AdSceneEntriesCount", sceneEntriesCount);

            if (PlayerPrefs.GetInt("AdSceneEntriesCount") >= 3)
            {
                StartCoroutine(resultPageAD(1));
                PlayerPrefs.SetInt("AdSceneEntriesCount", 0);
            }
            else
            {
                StartCoroutine(resultPage(1));
            }
        }
    }

    // Method to double the deeds
    public void DoubleDeedsPowerUp()
    {
        // Double the deeds
        int i = questionSetup.counter;
        rightDeeds *= 2;

        // Save the doubled deeds for the current question
        PlayerPrefs.SetFloat("Deeds_" + i, Mathf.Round(rightDeeds * 100f) / 100f);

        // Reset the power-up activation
        PlayerPrefs.SetInt("DoubleDeedPU", 0);
    }

    IEnumerator turnGreen(float seconds)
    {
        buttonP1.color = correctColor1;
        buttonP2.color = correctColor2;
        yield return new WaitForSeconds(seconds);
        buttonP1.color = defaultColor1;
        buttonP2.color = pickColor.GetComponent<Image>().color;
        PlayerPrefs.SetInt("currentExp", Mathf.Max(0, PlayerPrefs.GetInt("currentExp") + 5));
        PlayerPrefs.SetInt("totalExp", Mathf.Max(0, PlayerPrefs.GetInt("totalExp") + 5));
    }

    IEnumerator turnRed(float seconds)
    {
        buttonP1.color = wrongColor1;
        buttonP2.color = wrongColor2;
        yield return new WaitForSeconds(seconds);
        buttonP1.color = defaultColor1;
        buttonP2.color = pickColor.GetComponent<Image>().color;
        PlayerPrefs.SetInt("currentExp", Mathf.Max(0, PlayerPrefs.GetInt("currentExp") + 5));
        PlayerPrefs.SetInt("totalExp", Mathf.Max(0, PlayerPrefs.GetInt("totalExp") + 5));
    }

    IEnumerator reload(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        questionSetup.Start();
        timeController.ResetTimer(); // Reset the timer for the next question
        timeController.StartTimer(); // Start the timer for the next question
    }

    IEnumerator resultPage(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Ensure total score is not negative before displaying it
        int totalScore = PlayerPrefs.GetInt("totalExp");
        if (totalScore < 0)
        {
            PlayerPrefs.SetInt("totalExp", 0);
        }

        SceneManager.LoadScene("Result");
    }

    IEnumerator resultPageAD(int waitTime)
    {
        admobAdsScript.ShowInterstitialAd();
        yield return new WaitForSeconds(waitTime);

        // Ensure total score is not negative before displaying it
        int totalScore = PlayerPrefs.GetInt("totalExp");
        if (totalScore < 0)
        {
            PlayerPrefs.SetInt("totalExp", 0);
        }

        SceneManager.LoadScene("Result");
    }

    IEnumerator debounce(float waitTime)
    {
        blockPanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        blockPanel.SetActive(false);
    }

    // Method to remove a specified number of random wrong answers
    public void RemoveRandomWrongAnswers(int count)
    {
        // Get all answer buttons in the scene
        answerButton[] answerButtons = FindObjectsOfType<answerButton>();

        // Filter out the wrong answer buttons
        answerButton[] wrongAnswerButtons = answerButtons.Where(button => !button.isCorrect).ToArray();

        // If there are fewer wrong answers than the count, set count to the number of wrong answers
        count = Mathf.Min(count, wrongAnswerButtons.Length);

        // Shuffle the wrong answer buttons array
        ShuffleArray(wrongAnswerButtons);

        // Disable the specified number of wrong answer buttons
        for (int i = 0; i < count; i++)
        {
            wrongAnswerButtons[i].gameObject.SetActive(false);
        }
    }

    // Method to shuffle an array
    private void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}