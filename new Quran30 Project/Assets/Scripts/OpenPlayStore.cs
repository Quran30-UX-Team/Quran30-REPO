using UnityEngine;

public class OpenPlayStore : MonoBehaviour
{
    // Replace with your Play Store URL
    private string playStoreUrl = "https://play.google.com/store/apps/details?id=com.aqwise.quranirab&hl=en&gl=US";

    // Method to open the Play Store link
    public void OpenLink()
    {
        Application.OpenURL(playStoreUrl);
    }
}
