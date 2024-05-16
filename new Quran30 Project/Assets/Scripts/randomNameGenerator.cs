using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Random = UnityEngine.Random;

public class randomNameGenerator : MonoBehaviour
{
    public AudioManager audioManager;

    public TextAsset _adjFile;
    public TextAsset _nounFile;

    public string[] adjArray;
    public string[] nounArray;

    private string selectAdj;
    private string selectNoun;

    public int randomNumber;

    public string generatedName;
    public TMP_InputField displayName;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void OnValidate()
    {
        adjArray = _adjFile.text.Split('\n');
        nounArray = _nounFile.text.Split('\n');
    }
    public void randomizedName()
    {
        audioManager.PlaySFX(audioManager.changePageButtonSFX);

        selectAdj = adjArray[Random.Range(0, adjArray.Length)];
        selectNoun = nounArray[Random.Range(0, nounArray.Length)];

        randomNumber = Random.Range(0, 999);

        generatedName = selectAdj.Replace("\r", "").Replace("\n", "") + selectNoun.Replace("\r", "").Replace("\n", "") + randomNumber.ToString();
        displayName.text = generatedName;

        print(generatedName);
    }
}
