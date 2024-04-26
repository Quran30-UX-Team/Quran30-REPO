using UnityEngine;

public class doubleDeeds : MonoBehaviour
{
    private GameObject doublePanel;
    public void Start()
    {
        doublePanel = GameObject.Find("doublePanel");
    }
    public void onClick()
    {
        activateDouble();
    }
    public void activateDouble()
    {
        doublePanel.SetActive(true);

        // Find all answerButton components in the scene
        answerButton[] answerButtons = FindObjectsOfType<answerButton>();

        // Iterate through all answerButtons and call a function to activate the power-up
        foreach (answerButton button in answerButtons)
        {
            button.DoubleDeedsPowerUp();
        }

        this.gameObject.SetActive(false);
    }
}
