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

    public AudioSource src;
    public AudioClip rightSFX, wrongSFX;

    public doubleDeeds doubleDeeds;

    private bool isCorrect;
    private TextMeshProUGUI answerText;
    private Button buttonComponent;

    private questionSetup questionSetup;
    private timeController timeController;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;

    [SerializeField]
    private int score;

    private float rightDeeds;
    private float rightScores;

    private float wrongDeeds;
    private float wrongScores;

    private float rightDeedMulti = 0.5f;
    private float wrongDeedMulti = 0.05f;

    private void Awake()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        answerText = GetComponentInChildren<TextMeshProUGUI>();

        // Get the Button component attached to this GameObject
        buttonComponent = GetComponent<Button>();

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

        blockPanel = GameObject.Find("blockPanel");
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
        StartCoroutine(debounce(1));

        int i = questionSetup.counter;
        doublePanel.SetActive(false);

        if (isCorrect)
        {
            src.clip = rightSFX;
            src.Play();
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
            StartCoroutine(turnGreen());
        }
        else
        {
            src.clip = wrongSFX;
            src.Play();

            wrongDeeds = PlayerPrefs.GetFloat("Timer") * 0.05f;
            wrongScores = (11f - PlayerPrefs.GetFloat("Timer")) * -0.75f;

            Debug.Log("Wrong Timer: " + PlayerPrefs.GetFloat("Timer"));
            Debug.Log("Wrong Deeds: " + wrongDeeds);
            Debug.Log("Wrong Scores: " + wrongScores);

            // Save deeds and scores for the current question
            PlayerPrefs.SetFloat("Deeds_" + i, Mathf.Round(wrongDeeds * 100f) / 100f);
            PlayerPrefs.SetFloat("Scores_" + i, Mathf.Round(wrongScores * 100f) / 100f);

            // Change the button's color to red
            StartCoroutine(turnRed());
        }

        if (questionSetup.questions.Count > 0) // Changed the condition here
        {
            StartCoroutine(reload());
        }
        else
        {
            // If there is only one question left, change the scene
            StartCoroutine(resultPage(1));
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

    IEnumerator turnGreen()
    {
        buttonComponent.image.color = correctColor;
        yield return new WaitForSeconds(0.75f);
        buttonComponent.image.color = defaultColor;
        PlayerPrefs.SetInt("currentExp", PlayerPrefs.GetInt("currentExp") + 5);
        PlayerPrefs.SetInt("totalExp", PlayerPrefs.GetInt("totalExp") + 5);
    }

    IEnumerator turnRed()
    {
        buttonComponent.image.color = wrongColor;
        yield return new WaitForSeconds(0.75f);
        buttonComponent.image.color = defaultColor;
        PlayerPrefs.SetInt("currentExp", PlayerPrefs.GetInt("currentExp") + 5);
        PlayerPrefs.SetInt("totalExp", PlayerPrefs.GetInt("totalExp") + 5);
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1);
        questionSetup.Start();
        timeController.Start();
    }

    IEnumerator resultPage(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Result");
    }

    IEnumerator debounce(int waitTime)
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
