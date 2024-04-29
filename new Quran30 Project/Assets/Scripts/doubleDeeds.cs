using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class doubleDeeds : MonoBehaviour
{
    private GameObject doublePanel;

    public Button powerUPBtn;

    public void Start()
    {
        doublePanel = GameObject.Find("doublePanel");
        PlayerPrefs.SetInt("DoubleDeedPU", 0);
    }

    public void onClick()
    {
        doublePanel.SetActive(true);

        PlayerPrefs.SetInt("DoubleDeedPU", 1);
        powerUPBtn.interactable = false;
    }

}
