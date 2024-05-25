using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectedThemeHandler : MonoBehaviour
{
    private ThemeManager ThemeManager;

    public GameObject themeRing1;
    public GameObject themeRing2;
    public GameObject themeRing3;
    public GameObject themeRing4;

    public void Update()
    {
        switch (PlayerPrefs.GetInt("choosenTheme"))
        {
            case 1:
                SetAlpha(themeRing1, 1f);
                SetAlpha(themeRing2, 0f);
                SetAlpha(themeRing3, 0f);
                break;
            case 2:
                SetAlpha(themeRing1, 0f);
                SetAlpha(themeRing2, 1f);
                SetAlpha(themeRing3, 0f);
                break;
            case 3:
                SetAlpha(themeRing1, 0f);
                SetAlpha(themeRing2, 0f);
                SetAlpha(themeRing3, 1f);
                break;
            case 4:
                SetAlpha(themeRing1, 0f);
                SetAlpha(themeRing2, 0f);
                SetAlpha(themeRing3, 0f);
                break;
            default:
                SetAlpha(themeRing1, 1f);
                SetAlpha(themeRing2, 0f);
                SetAlpha(themeRing3, 0f);
                break;
        }
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        Image image = obj.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
        else
        {
            Debug.LogWarning("Image component not found on " + obj.name);
        }
    }
}
