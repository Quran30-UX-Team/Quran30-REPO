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

        if (SceneExists(sceneName))
        {
            StartCoroutine(delayLoadScene(0.2f, sceneName));
        }
        else
        {
            Debug.LogWarning("Scene " + sceneName + " does not exist!");
        }
    }

    IEnumerator delayLoadScene(float delay, string getScene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(getScene);
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (scene.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
