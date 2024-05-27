using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAllPlayerPref : MonoBehaviour
{
    // Call this method to reset PlayerPrefs and load the next scene
    public void ResetPrefsAndLoadScene()
    {
        // Delete all PlayerPrefs
        PlayerPrefs.DeleteAll();

        // Save the changes to ensure PlayerPrefs are cleared
        PlayerPrefs.Save();

        StartCoroutine(WaitToLoad(1));
    }

    IEnumerator WaitToLoad(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Load the specified scene
        SceneManager.LoadScene("Startup");
    }
}
