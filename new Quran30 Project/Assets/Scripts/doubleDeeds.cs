using UnityEngine;

public class doubleDeeds : MonoBehaviour
{
    public GameObject doublePanel;

    public void onClick()
    {
        if (PlayerPrefs.GetFloat("Premium") > 9)
        {
            PlayerPrefs.SetFloat("Premium", PlayerPrefs.GetFloat("Premium") - 10);
            activateDouble();
        }
        else
        {
            print("No Money");
        }
    }
    public void activateDouble()
    {
        doublePanel.SetActive(true);

        // Find all answerButton components in the scene
        answerButton[] answerButtons = FindObjectsOfType<answerButton>();

        // Iterate through all answerButtons and call a function to activate the power-up
        foreach (answerButton button in answerButtons)
        {
            button.ActivatePowerUp();
        }
    }
}
