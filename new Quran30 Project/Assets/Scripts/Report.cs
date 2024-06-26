using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Report : MonoBehaviour
{
    [SerializeField] TMP_InputField feedbackField;
    [SerializeField] Toggle[] checkboxes; // 5 checkboxes
    [SerializeField] GameObject thankYouPanel;

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScr1OjqHfZiikiwngDbypnuQOZjjJy-rhaf6wePq3u1zsj5-w/formResponse";

    void Start()
    {
        // Initialize checkboxes
        foreach (Toggle checkbox in checkboxes)
        {
            checkbox.isOn = false;
        }
    }

    public void Send()
    {
        StartCoroutine(Post(feedbackField.text, GetCheckboxAnswers()));
        thankYouPanel.SetActive(true);
    }

    IEnumerator Post(string _feedback, string _checkboxAnswers)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.811636940", _feedback);
        form.AddField("entry.1318039022", _checkboxAnswers);

        Debug.Log("Request data:");
        Debug.Log("Feedback: " + _feedback);
        Debug.Log("Checkbox answers: " + _checkboxAnswers);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        www.timeout = 10; // Set a higher timeout value

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Report sent successfully!");
        }
        else
        {
            Debug.Log("Error sending report: " + www.error);
        }
    }

    string GetCheckboxAnswers()
    {
        List<string> checkboxAnswers = new List<string>();

        foreach (Toggle checkbox in checkboxes)
        {
            if (checkbox.isOn)
            {
                checkboxAnswers.Add(GetCheckboxAnswer(checkbox));
            }
        }

        // Return a comma-separated string
        return string.Join(" , ", checkboxAnswers);
    }

    string GetCheckboxAnswer(Toggle checkbox)
    {
        // Return the corresponding answer for each checkbox
        if (checkbox == checkboxes[0])
        {
            return "Typo in the question";
        }
        else if (checkbox == checkboxes[1])
        {
            return "Wrong image for the question";
        }
        else if (checkbox == checkboxes[2])
        {
            return "Typo on the answer";
        }
        else if (checkbox == checkboxes[3])
        {
            return "Image not appear properly";
        }
        else
        {
            return "Other:";
        }
    }
}