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
    public string ClaimedTag = "Claimed";

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
            bool xIsClaimed = x.CompareTag(ClaimedTag);
            bool yIsClaimed = y.CompareTag(ClaimedTag);

            if (xIsClaimed && !yIsClaimed)
            {
                return 1; // x is claimed, y is not claimed, so x should come after y
            }
            else if (!xIsClaimed && yIsClaimed)
            {
                return -1; // x is not claimed, y is claimed, so x should come before y
            }
            else // both x and y are either claimed or not claimed
            {
                if (xIsCompleted && !yIsCompleted)
                {
                    return -1; // x is completed, y is not completed, so x should come before y
                }
                else if (!xIsCompleted && yIsCompleted)
                {
                    return 1; // x is not completed, y is completed, so x should come after y
                }
                else
                {
                    return 0; // both x and y have the same completion status, so maintain their current order
                }
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
