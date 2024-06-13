using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitSlot : MonoBehaviour
{
    public SpriteRenderer bodyPart;
    public List<Sprite> options = new List<Sprite>();

    private int currentOption = 0;

    // void Start()
    //  {
    // LoadOption();//
    // }//

    public void SelectOption(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < options.Count)
        {
            currentOption = optionIndex;
            bodyPart.sprite = options[currentOption];
            SaveOption();
        }
    }

    public void SaveCurrentOption()
    {
        SaveOption();
    }

    private void SaveOption()
    {
        PlayerPrefs.SetInt("SelectedOption", currentOption);
    }

    public void LoadOption()
    {
        currentOption = PlayerPrefs.GetInt("SelectedOption", 0);
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
