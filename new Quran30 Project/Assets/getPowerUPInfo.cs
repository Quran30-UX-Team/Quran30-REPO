using UnityEngine;

public class getPowerUPInfo : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    public void Update()
    {
        // Get the name of the single child object of slot1, if any
        string powerUpName1 = "";
        if (slot1 != null)
        {
            if (slot1.transform.childCount > 0)
            {
                powerUpName1 = slot1.transform.GetChild(0).name;
            }
            else
            {
                powerUpName1 = "Empty";
            }
            PlayerPrefs.SetString("Slot1", powerUpName1);
        }

        // Get the name of the single child object of slot2, if any
        string powerUpName2 = "";
        if (slot2 != null)
        {
            if (slot2.transform.childCount > 0)
            {
                powerUpName2 = slot2.transform.GetChild(0).name;
            }
            else
            {
                powerUpName2 = "Empty";
            }
            PlayerPrefs.SetString("Slot2", powerUpName2);
        }

        // Get the name of the single child object of slot3, if any
        string powerUpName3 = "";
        if (slot3 != null)
        {
            if (slot3.transform.childCount > 0)
            {
                powerUpName3 = slot3.transform.GetChild(0).name;
            }
            else
            {
                powerUpName3 = "Empty";
            }
            PlayerPrefs.SetString("Slot3", powerUpName3);
        }

        Debug.Log(powerUpName1 + " " + powerUpName2 + " " + powerUpName3);
    }
}
