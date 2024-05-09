using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            // Check if the dropped item is EraseOnePU(Clone)
            if (dropped.name == "EraseOnePU(Clone)")
            {
                // Remove 5 premium
                if (PlayerPrefs.GetFloat("Premium") >= 5)
                {
                    PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") - 5);
                }
                else
                {
                    Debug.Log("Not enough premium to remove.");
                    return; // Exit the method if not enough premium
                }
            }
            else
            {
                // If not EraseOnePU(Clone), remove 10 premium
                if (PlayerPrefs.GetFloat("Premium") >= 10)
                {
                    PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") - 10);
                }
                else
                {
                    Debug.Log("Not enough premium to add.");
                    return; // Exit the method if not enough premium
                }
            }

            // Assign the parentAfterDrag and tag
            draggableItem.parentAfterDrag = transform;
            draggableItem.tag = "SelectedPowerUP";
        }
    }
}
