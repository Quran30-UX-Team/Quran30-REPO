using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PedestalHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI getName;
    [SerializeField] private TextMeshProUGUI getScore;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;
    void FixedUpdate()
    {
        playerName.text = getName.text;
        playerScore.text = getScore.text;
    }
}
