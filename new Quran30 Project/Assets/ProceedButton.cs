using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProceedButton : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private GameObject errorText;

    private bool isProcessingClick = false;
    private Vector3 originalPosition;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        // Ensure the error text is initially hidden
        errorText.SetActive(false);
        // Store the original position of the error text
        originalPosition = errorText.transform.localPosition;
    }

    public void onClick()
    {
        if (isProcessingClick) return;

        // Start processing click
        isProcessingClick = true;

        // Check if the input field text is not empty or whitespace
        if (!string.IsNullOrWhiteSpace(nameField.text))
        {
            StartCoroutine(delayLoadScene(0.2f));
            if (!PlayerPrefs.HasKey("FirstLaunch"))
            {

                // Set flag to indicate app has been launched
                PlayerPrefs.SetInt("FirstLaunch", 1);
            }
        }
        else
        {
            Debug.Log("Field is empty. Please enter a name.");
            errorText.SetActive(true);
            // Start the coroutine to animate and hide the error text after a delay
            audioManager.PlaySFX(audioManager.changePageButtonSFX);
            StartCoroutine(AnimateAndHideErrorText(1.0f));
        }

        // End processing click after a delay (debounce time)
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        StartCoroutine(EndProcessingClickAfterDelay(1.0f));
    }

    private IEnumerator AnimateAndHideErrorText(float delay)
    {
        // Duration of the upward movement animation
        float animationDuration = 0.5f;
        // Height to move upwards
        float upwardDistance = 30.0f;

        // Animate moving upwards
        Vector3 targetPosition = originalPosition + new Vector3(0, upwardDistance, 0);
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            errorText.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        errorText.transform.localPosition = targetPosition;

        // Wait for the remaining delay time before hiding the error text
        yield return new WaitForSeconds(delay - animationDuration);

        // Hide the error text and reset its position
        errorText.SetActive(false);
        errorText.transform.localPosition = originalPosition;
    }

    private IEnumerator EndProcessingClickAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessingClick = false;
    }

    IEnumerator delayLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
