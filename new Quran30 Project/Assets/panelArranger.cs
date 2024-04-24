using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelArranger : MonoBehaviour
{
    // Reference to the parent transform containing all the panels
    public Transform panelsParent;

    // Tag to identify completed achievement panels
    public string completedAchievementTag = "CompletedAchievement";

    // Function to reorder the panels
    void ReorderPanels()
    {
        // Get all panels under the parent transform
        List<Transform> panels = new List<Transform>();
        foreach (Transform child in panelsParent)
        {
            panels.Add(child);
        }

        // Sort the panels based on their tag
        panels.Sort((x, y) =>
        {
            bool xIsCompleted = x.CompareTag(completedAchievementTag);
            bool yIsCompleted = y.CompareTag(completedAchievementTag);

            if (xIsCompleted && !yIsCompleted)
            {
                return 1;
            }
            else if (!xIsCompleted && yIsCompleted)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });

        // Reposition the panels according to the sorted order
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetSiblingIndex(i);
        }
    }

    // Call this function to reorder panels whenever needed
    public void Update()
    {
        ReorderPanels();
    }
}
