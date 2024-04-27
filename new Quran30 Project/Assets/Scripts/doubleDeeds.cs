using System.Collections;
using UnityEngine;

public class doubleDeeds : MonoBehaviour
{
    private GameObject doublePanel;

    public void Start()
    {
        doublePanel = GameObject.Find("doublePanel");
        PlayerPrefs.SetInt("DoubleDeedPU", 0);
    }

    public void onClick()
    {
        doublePanel.SetActive(true);

        PlayerPrefs.SetInt("DoubleDeedPU", 1);
        this.gameObject.SetActive(false);
    }

}
