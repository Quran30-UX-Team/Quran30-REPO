using UnityEngine;

public class HaveEnteredQuizScript : MonoBehaviour
{
    private void Start()
    {
        // Increment scene entries count when the scene is entered
        IncrementSceneEntriesCount();
    }

    private void IncrementSceneEntriesCount()
    {
        int sceneEntriesCount = PlayerPrefs.GetInt("SceneEntriesCount");
        sceneEntriesCount++;
        PlayerPrefs.SetInt("SceneEntriesCount", sceneEntriesCount);
    }
}