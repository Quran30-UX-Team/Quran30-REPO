using UnityEngine;

public class AvatarSaver : MonoBehaviour
{
    public OutfitSlot[] outfitSlots;

    private void Start()
    {
        AssignSaveKeys();
        LoadSavedOutfit();
    }

    public void SaveOutfit()
    {
        foreach (OutfitSlot outfitSlot in outfitSlots)
        {
            int optionIndex = outfitSlot.currentOption;
            outfitSlot.SelectOption(optionIndex);
            Debug.Log("Saved OutfitSlot with key: " + outfitSlot.saveKey + " and option index: " + optionIndex);
        }
    }

    private void LoadSavedOutfit()
    {
        foreach (OutfitSlot outfitSlot in outfitSlots)
        {
            outfitSlot.LoadOption();
        }
    }

    private void AssignSaveKeys()
    {
        for (int i = 0; i < outfitSlots.Length; i++)
        {
            outfitSlots[i].saveKey = "outfitSlot_" + i;
        }
    }
}