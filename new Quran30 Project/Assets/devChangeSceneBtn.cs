using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class devChangeSceneBtn : MonoBehaviour
{
    public void onClick()
    {
        if (DevBtn.devMode != false)
        {
            SceneManager.LoadScene("Leaderboard");
        }
        else
        {
            SceneManager.LoadScene("COMING SOON");
        }
    }
}
