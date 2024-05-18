using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class doubleDeeds : MonoBehaviour
{
    private GameObject doublePanel;
    AudioManager audioManager;
    public GameObject blockPanel;

    public Button powerUPBtn;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        blockPanel = GameObject.Find("PUBlockPanel");
    }

    public void Start()
    {
        doublePanel = GameObject.Find("doublePanel");
        PlayerPrefs.SetInt("DoubleDeedPU", 0);
    }

    public void onClick()
    {
        blockPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.changePageButtonSFX);
        doublePanel.SetActive(true);

        PlayerPrefs.SetInt("DoubleDeedPU", 1);
        powerUPBtn.interactable = false;
    }

}
