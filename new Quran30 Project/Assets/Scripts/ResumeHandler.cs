using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeHandler : MonoBehaviour
{
    public pauseHandler pauseHandler; // Reference to your pauseHandler script

    public Button resumeButton; // Reference to the Button component

    private void Start()
    {
        // Initialize the button click event
        resumeButton.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame()
    {
        // Call the pauseToggle method to resume the game
        pauseHandler.pauseToggle();
    }
}
