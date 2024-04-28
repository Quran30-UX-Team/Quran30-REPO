using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class givePowerUP : MonoBehaviour
{
    public GameObject FreezePU;
    public GameObject DoublePU;
    public GameObject HalfPU;

    void Start()
    {
        // Check the value of PlayerPrefs for each slot and set the child object accordingly
        SetChildObject(PlayerPrefs.GetString("Slot1"), transform);
        SetChildObject(PlayerPrefs.GetString("Slot2"), transform);
        SetChildObject(PlayerPrefs.GetString("Slot3"), transform);
    }

    private void SetChildObject(string powerUpName, Transform parent)
    {
        // If the slot is empty, do nothing
        if (powerUpName == "Empty")
        {
            return;
        }

        GameObject powerUpPrefab = null;

        // Choose the appropriate power-up prefab based on the powerUpName
        switch (powerUpName)
        {
            case "FreezePU(Clone)":
                powerUpPrefab = FreezePU;
                break;
            case "DoubleDeedPU(Clone)":
                powerUpPrefab = DoublePU;
                break;
            case "HalfPU(Clone)":
                powerUpPrefab = HalfPU;
                break;
            default:
                Debug.LogWarning("Unknown power-up: " + powerUpName);
                break;
        }

        if (powerUpPrefab != null)
        {
            // Instantiate the provided power-up prefab as a child object of the specified parent
            GameObject child = Instantiate(powerUpPrefab, parent);
            child.transform.localPosition = Vector3.zero; // Reset local position to (0, 0, 0)
        }
    }
}
