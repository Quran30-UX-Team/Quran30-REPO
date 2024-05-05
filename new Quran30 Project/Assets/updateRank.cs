using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System;
using Dan.Models;

public class updateRank : MonoBehaviour
{
    public TextMeshProUGUI rankNumberText;
    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.Quran30LeaderboardTest.GetEntries(OnEntriesLoaded, OnError);
    }

    private void OnEntriesLoaded(Entry[] entries)
    {
        foreach (Entry entry in entries)
        {
            string rankSuffix = entry.RankSuffix();

            // Returns true if the entry belongs to the player
            bool isMine = entry.IsMine();

            if (isMine)
            {
                rankNumberText.text = $"{rankSuffix}";
            }
        }
    }

    private void OnError(string error)
    {
        Debug.LogError(error);
    }
}
