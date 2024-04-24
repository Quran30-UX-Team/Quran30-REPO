using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class panelArranger : MonoBehaviour
{
    // Reference to the parent transform containing all the panels
    public Transform panelsParent;

    // Tag to identify completed achievement panels
    public string completedAchievementTag = "CompletedAchievement";

    private int totalPanels;
    private int panelsWithTag;

    // Reference to the TextMeshProUGUI component to display the count
    public TextMeshProUGUI countText;

    // Function to reorder the panels and update count text
    void ReorderPanels()
    {
        // Get all panels under the parent transform
        List<Transform> panels = new List<Transform>();
        panelsWithTag = 0; // Reset the count of panels with tag

        foreach (Transform child in panelsParent)
        {
            panels.Add(child);

            // Check if the panel has the specified tag
            if (child.CompareTag(completedAchievementTag))
            {
                panelsWithTag++;
            }
        }

        totalPanels = panels.Count;

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

        // Update count text
        UpdateCountText();
    }

    // Method to update count text
    void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = panelsWithTag + " / " + totalPanels;
        }
    }

    // Call this function to reorder panels whenever needed
    public void Update()
    {
        ReorderPanels();
    }
}
