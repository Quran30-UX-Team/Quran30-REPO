using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class UIContainerPrefabCreator : MonoBehaviour
{
    public GameObject uiPrefab; // UI Prefab to instantiate
    public int numberOfPrefabs = 500; // Number of prefabs to create
    public RectTransform container; // Vertical UI container

#if UNITY_EDITOR
    void Update()
    {
        CreatePrefabs();
    }
#endif

    public void CreatePrefabs()
    {
        if (uiPrefab == null || container == null)
            return;

        // Clear existing prefabs
        foreach (Transform child in container)
        {
            DestroyImmediate(child.gameObject);
        }

        // Loop through the number of prefabs
        for (int i = 1; i <= numberOfPrefabs; i++)
        {
            // Instantiate UI prefab
            GameObject newUIPrefab = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);

            // Set name of the TextMeshPro text object to the current number
            TextMeshProUGUI textMeshPro = newUIPrefab.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = i.ToString();
            }
            else
            {
                Debug.LogWarning("TextMeshProUGUI component not found in the UI prefab.");
            }

            // Parent the new UI prefab to the container
            newUIPrefab.transform.SetParent(container, false);
        }

        // Sort UI prefabs based on the text content
        SortUIPrefabs();
    }

    void SortUIPrefabs()
    {
        // Get all child objects of the container
        Transform[] children = new Transform[container.childCount];
        for (int i = 0; i < container.childCount; i++)
        {
            children[i] = container.GetChild(i);
        }

        // Sort children based on the name (assuming all names are numbers)
        System.Array.Sort(children, (x, y) => int.Parse(x.GetComponentInChildren<TextMeshProUGUI>().text).CompareTo(int.Parse(y.GetComponentInChildren<TextMeshProUGUI>().text)));

        // Re-parent the sorted children
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}
