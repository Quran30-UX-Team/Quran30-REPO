using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObject/QuestionData", order = 1)]
public class questionData : ScriptableObject
{
    public string questionCode;

    public string juz;

    public string surahName;

    public string category;

    public Texture2D image;

    public string question;

    [Tooltip("First slot is the answer")]
    public string[] answers;
}
