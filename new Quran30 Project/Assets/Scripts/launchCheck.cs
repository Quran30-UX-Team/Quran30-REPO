using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class launchCheck : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("FirstLaunch"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
