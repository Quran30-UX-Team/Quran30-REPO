using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTypeSelecter : MonoBehaviour
{
    public void loadLevelType()
    {
        PlayerPrefs.GetString("Level Type");
        print(PlayerPrefs.GetString("Level Type"));
        SceneManager.LoadScene("SurahSelect");
    }
}
