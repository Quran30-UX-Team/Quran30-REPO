using UnityEngine;
using System.Collections.Generic;

public class OutfitSlot : MonoBehaviour
{
    public SpriteRenderer bodyPart;
    public List<Sprite> options = new List<Sprite>();

    private string saveKey;

    public OutfitSlot(string saveKey)
    {
        this.saveKey = saveKey;
    }

  

    public int currentOption
    {
        get { return PlayerPrefs.GetInt(saveKey, 0); }
        set { PlayerPrefs.SetInt(saveKey, value); }
    }

    public void SelectOption(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < options.Count)
        {
            currentOption = optionIndex;
            bodyPart.sprite = options[currentOption];
        }
    }

    public void LoadOption()
    {
        if (currentOption >= 0 && currentOption < options.Count)
        {
            bodyPart.sprite = options[currentOption];
            Debug.Log("Loaded option: " + currentOption);
        }
        else
        {
            Debug.LogWarning("Loaded option index is out of range: " + currentOption);
        }
    }
}