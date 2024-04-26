using UnityEngine;

public class duplicatePowerUP : MonoBehaviour
{
    public GameObject prefab;

    void FixedUpdate()
    {
        // Check if there's more than one child
        if (transform.childCount > 1)
        {
            // Destroy extra children
            for (int i = 1; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        // Check if there are no children
        else if (transform.childCount == 0)
        {
            // Instantiate the prefab if there are no children
            GameObject newPrefab = Instantiate(prefab, transform);
            newPrefab.transform.localPosition = Vector3.zero; // Set the local position to (0, 0, 0)

            // Get the DraggableItem component from the newPrefab
            DraggableItem draggableItem = newPrefab.GetComponent<DraggableItem>();

            // Set the parentAfterDrag property to the parent of the newPrefab
            draggableItem.parentAfterDrag = transform;

            // Check if the DraggableItem script is attached to the newPrefab
            if (draggableItem != null)
            {
                // Set raycastTarget to true for the draggableItem's image
                if (draggableItem.image != null)
                {
                    draggableItem.image.raycastTarget = true;
                }
                else
                {
                    Debug.LogWarning("Image component not found on DraggableItem script. Make sure it is assigned in the Inspector.");
                }
            }
            else
            {
                Debug.LogWarning("DraggableItem script not found on the newPrefab. Make sure it is attached.");
            }
        }
    }
}
