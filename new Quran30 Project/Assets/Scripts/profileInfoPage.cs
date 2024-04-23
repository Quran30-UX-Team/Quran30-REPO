using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class profileInfoPage : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI deeds;

    private void Start()
    {
        username.text = PlayerPrefs.GetString("Profile Name");
        deeds.text = PlayerPrefs.GetInt("Deeds").ToString();
    }
}
