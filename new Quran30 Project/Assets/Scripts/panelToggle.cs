using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class panelToggle : MonoBehaviour
{
    public GameObject panel;
    public bool isShow;

    public void togglePanel()
    {
        if (panel.activeSelf == false)
        {
            panel.SetActive(true);
        }

        else
        {
            panel.SetActive(false);
        }
    }
}
