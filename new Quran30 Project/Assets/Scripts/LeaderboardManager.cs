using UnityEngine;
using TMPro;
using Dan.Main;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public List<TextMeshProUGUI> usernames;
    public List<TextMeshProUGUI> scores;
    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.Quran30LeaderboardTest.GetEntries(entries =>
        {
            foreach (TextMeshProUGUI name in usernames)
            {
                name.text = "";
            }

            foreach (TextMeshProUGUI score in scores)
            {
                score.text = "";
            }

            var length = Mathf.Min(usernames.Count, entries.Length);

            for (int i = 0; i < length; i++)
            {
                usernames[i].text = entries[i].Username;
                scores[i].text = entries[i].Score.ToString();
            }
        });

    }

    public void SetEntry(string username, int score)
    {
        Leaderboards.Quran30LeaderboardTest.UploadNewEntry(username, score, isSuccessful =>
        {
            if (isSuccessful)
            {
                LoadEntries();
            }
        });
    }
}
