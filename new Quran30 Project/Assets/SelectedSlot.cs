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
            draggableItem.parentAfterDrag = transform;
            draggableItem.tag = "SelectedPowerUP";
            PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") - 10);
        }
    }
}
