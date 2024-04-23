using System.Collections;
using UnityEngine;

public class DevBtn : MonoBehaviour
{
    private int clickCount = 0;
    private Coroutine resetCoroutine;

    public static bool devMode = false;

    public GameObject panel;

    public void GiveDev()
    {
        clickCount++;
        print("CLICK");
        print(clickCount);

        if (clickCount > 6)
        {
            print("You are DEV");
            devMode = true;
            StartCoroutine(showPanel());
            PlayerPrefs.SetFloat("Deeds", 9999);
            PlayerPrefs.SetFloat("Premium", 9999);

        }

        // If the coroutine is already running, stop it
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }

        // Start the coroutine to reset the click count after 3 seconds
        resetCoroutine = StartCoroutine(ResetClick());
    }

    IEnumerator ResetClick()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // If the click count is not 7, reset it
        if (clickCount != 7)
        {
            clickCount = 0;
            print("Click count reset");
        }
        // Reset the coroutine reference
        resetCoroutine = null;
    }

    IEnumerator showPanel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
    }
}
