using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;
    Camera mainCamera;

    public Image image;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Check if the draggable object has any tag assigned to it
        if (gameObject.tag == "SelectedPowerUP")
        {
            // If the object has any tag, destroy it
            Destroy(gameObject);
            return; // Exit the method to prevent further execution
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen coordinates to world coordinates
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Keep the original Z position
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset its parent and enable raycast target
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
