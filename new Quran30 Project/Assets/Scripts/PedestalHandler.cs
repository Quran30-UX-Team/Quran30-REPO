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
    void Start()
    {
        StartCoroutine(getInfo());
    }

    IEnumerator getInfo()
    {
        yield return new WaitForSeconds(1);
        playerName.text = getName.text;
        playerScore.text = getScore.text;
    }
}
