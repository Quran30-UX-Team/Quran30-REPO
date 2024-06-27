using System.Collections.Generic;

[System.Serializable]
public class AvatarData
{
    public List<int> selectedOptions = new List<int>();

    public AvatarData(OutfitSlot[] outfitSlots)
    {
        for (int i = 0; i < outfitSlots.Length; i++)
        {
            selectedOptions.Add(outfitSlots[i].currentOption);
        }
    }
}