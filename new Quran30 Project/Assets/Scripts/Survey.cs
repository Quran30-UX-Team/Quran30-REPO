using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Survey : MonoBehaviour
{
    [SerializeField] TMP_InputField feedbackField;
    [SerializeField] TMP_Text messageText;
    [SerializeField] Button[] ratingButtons; // Buttons representing the rating scale

    private int selectedRating = 0;
    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfn2Y0KkWSdkeoS51q9Z18nWNnJmtDvNZ72N9gvtotKmL4GWw/formResponse";

    void Start()
    {
        // Assign a listener to each rating button
        for (int i = 0; i < ratingButtons.Length; i++)
        {
            int index = i + 1; // Ratings start from 1 to 5
            ratingButtons[i].onClick.AddListener(() => SetRating(index));
        }
    }

    public void SetRating(int rating)
    {
        selectedRating = rating;
        // Optional: Highlight the selected rating button for better UX
        foreach (Button button in ratingButtons)
        {
            button.interactable = true;
        }
        ratingButtons[rating - 1].interactable = false;
    }

    public void Send()
    {
        if (selectedRating == 0)
        {
            messageText.text = "Please select a rating.";
        }
        else if (string.IsNullOrEmpty(feedbackField.text))
        {
            messageText.text = "Please input some text.";
        }
        else
        {
            messageText.text = "";
            StartCoroutine(Post(feedbackField.text, selectedRating));
        }
    }

    IEnumerator Post(string _feedback, int _rating)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.519873381", _feedback); // Replace with your feedback field entry ID
        form.AddField("entry.1323601311", _rating.ToString()); // Replace with your rating field entry ID

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            messageText.text = "Feedback sent successfully!";
        }
        else
        {
            messageText.text = "Failed to send feedback.";
        }
    }
}
