using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayCoin : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        // Get the updated deeds value from PlayerPrefs
        float updatedDeeds = PlayerPrefs.GetFloat("Deeds", 0);
        scoreText.text = Mathf.RoundToInt(updatedDeeds).ToString(); // Update the displayed text
    }
}