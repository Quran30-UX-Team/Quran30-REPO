using UnityEngine;
using UnityEngine.Localization.Settings;

public class langChanger : MonoBehaviour
{
    public static string SelectedLocaleKey = "SelectedLocale";

    public void ChangeLocalization(string localeCode)
    {
        // Switch to the specified locale
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        // Save the selected locale identifier
        PlayerPrefs.SetString(SelectedLocaleKey, localeCode);
    }

    private void Start()
    {
        // Load the selected locale identifier from PlayerPrefs if it exists
        if (PlayerPrefs.HasKey(SelectedLocaleKey))
        {
            string savedLocale = PlayerPrefs.GetString(SelectedLocaleKey);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(savedLocale);
        }
    }
}
