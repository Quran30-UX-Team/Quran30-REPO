using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_MANAGER : MonoBehaviour
{
    private static GAME_MANAGER instance;
    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and make it persistent
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }
}
