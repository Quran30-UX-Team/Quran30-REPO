using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SurahNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI surahNameText;

    private void Start()
    {
        string surahName = PlayerPrefs.GetString("Question Set");
        surahNameText.text = surahName;
    }
}