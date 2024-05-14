using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void loadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        StartCoroutine(delayLoadScene(0.2f, sceneName));
    }

    IEnumerator delayLoadScene(float delay, string getScene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(getScene);
    }
}
