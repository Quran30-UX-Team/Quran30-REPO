using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelPage : MonoBehaviour
{
    public GameObject[] verticalScrollViews; // Array to hold references to vertical scroll views

    private void Start()
    {
        // Initially disable all vertical scroll views except the first one
        for (int i = 1; i < verticalScrollViews.Length; i++)
        {
            verticalScrollViews[i].SetActive(false);
        }
        verticalScrollViews[0].SetActive(true); // Ensure the first vertical scroll view is active
    }

    // Method to be called when a horizontal panel is clicked
    public void ShowVerticalScrollView(int index)
    {
        // Disable all vertical scroll views
        foreach (GameObject scrollView in verticalScrollViews)
        {
            scrollView.SetActive(false);
        }

        // Enable the selected vertical scroll view
        if (index >= 0 && index < verticalScrollViews.Length)
        {
            verticalScrollViews[index].SetActive(true);
        }
    }
}
