using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RateUsPopup : MonoBehaviour
{
    public Button[] starButtons;
    public Button rateButton;
    public Button notNowButton;
    public GameObject rateUsPanel;
    private string playStoreUrl = "https://play.google.com/store/apps/details?id=com.aqwise.quranirab&hl=en_US";

    private int selectedStarIndex = -1; // Keep track of the highest selected star index

    void Start()
    {
        if (starButtons == null || starButtons.Length == 0 || rateButton == null || notNowButton == null || rateUsPanel == null)
        {
            Debug.LogError("One or more UI elements are not assigned in the Inspector.");
            return;
        }

        rateButton.onClick.AddListener(OpenPlayStore);
        notNowButton.onClick.AddListener(ClosePopup);

        for (int i = 0; i < starButtons.Length; i++)
        {
            int index = i;
            starButtons[i].onClick.AddListener(() => OnStarClicked(index));
        }
    }

    public void ShowPopup()
    {
        rateUsPanel.SetActive(true);
    }

    private void OnStarClicked(int index)
    {
        selectedStarIndex = index;

        for (int i = 0; i < starButtons.Length; i++)
        {
            if (i <= selectedStarIndex)
            {
                starButtons[i].image.color = Color.yellow; // Highlight the star
            }
            else
            {
                starButtons[i].image.color = Color.white; // Un-highlight the star
            }
        }
    }

    private void OpenPlayStore()
    {
        Application.OpenURL(playStoreUrl);
        rateUsPanel.SetActive(false);
    }

    private void ClosePopup()
    {
        rateUsPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}
