using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class saveName : MonoBehaviour
{
    public TMP_InputField fieldText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("FirstLaunch"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
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
}
