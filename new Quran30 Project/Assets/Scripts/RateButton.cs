using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateButton : MonoBehaviour
{
    public Button rateButton;
    public RateUsPopup rateUsPopup;

    void Start()
    {
        rateButton.onClick.AddListener(OnRateButtonClick);
    }

    private void OnRateButtonClick()
    {
        rateUsPopup.ShowPopup();
    }


}
