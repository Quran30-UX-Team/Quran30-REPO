using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDescription : MonoBehaviour
{
    public GameObject otherBtn;

    public GameObject description;
    public Button button;

    public bool isShow = false;
    private RectTransform otherBtnRectTransform;

    private void Start()
    {
        button.interactable = false;
        otherBtnRectTransform = otherBtn.GetComponent<RectTransform>();
    }

    public void ControlDesc(string levelType)
    {
        if (isShow == false)
        {
            PlayerPrefs.SetString("Level Type", levelType);
            print(PlayerPrefs.GetString("Level Type"));
            ShowDesc();
            isShow = true;

            // Move the other button down
            if (otherBtnRectTransform != null)
            {
                otherBtnRectTransform.anchoredPosition -= new Vector2(0, 50);
            }

            else
            {
                Debug.Log("No Button");
            }
        }
        else
        {
            HideDesc();
            isShow = false;

            // Move the other button back up
            if (otherBtnRectTransform != null)
            {
                otherBtnRectTransform.anchoredPosition += new Vector2(0, 50);
            }

            else
            {
                Debug.Log("No Button");
            }
        }
    }

    public void ShowDesc()
    {
        description.SetActive(true);
        button.interactable = true;
        isShow = true;
    }

    public void HideDesc()
    {
        description.SetActive(false);
        button.interactable = false;
        isShow = false;
    }
}

