using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayPremium : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Update()
    {
        scoreText.text = Mathf.RoundToInt(PlayerPrefs.GetFloat("Premium")).ToString();
    }
}
