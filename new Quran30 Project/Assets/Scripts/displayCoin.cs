using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayCoin : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = Mathf.RoundToInt(PlayerPrefs.GetFloat("Deeds")).ToString();
    }

}
