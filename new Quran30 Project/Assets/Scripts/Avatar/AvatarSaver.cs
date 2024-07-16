using UnityEngine;
using System.Collections.Generic;

public class AvatarSaver : MonoBehaviour
{
    public OutfitSlot[] outfitSlots;

    private void Start()
    {
        LoadSavedOutfit();
    }

    public void SaveOutfit()
    {
        foreach (OutfitSlot outfitSlot in outfitSlots)
        {
            outfitSlot.SelectOption(outfitSlot.options.IndexOf(outfitSlot.bodyPart.sprite));
        }
    }

    private void LoadSavedOutfit()
    {
        foreach (OutfitSlot outfitSlot in outfitSlots)
        {
            outfitSlot.LoadOption();
        }
    }
}