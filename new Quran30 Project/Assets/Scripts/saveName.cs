using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class saveName : MonoBehaviour
{
    public TMP_InputField fieldText;
    public void saveGeneratedName()
    {
        if (fieldText.text == "")
        {
            PlayerPrefs.SetString("Profile Name", "Guest");
        }

        else
        {
            PlayerPrefs.SetString("Profile Name", fieldText.text);
        }

    }

    private void Start()
    {
        // Check if it's the first time the app is launched
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {

            // Set flag to indicate app has been launched
            PlayerPrefs.SetInt("FirstLaunch", 1);
        }
        else
        {
            // If not the first launch, load the main scene directly
            SceneManager.LoadScene("MainMenu");
        }
    }

}
