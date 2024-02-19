using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUIItem : MonoBehaviour
{
    [SerializeField] RectTransform scrollviewContent;

    private void Start()
    {
        Leaderboard.instance.scrollviewContent = scrollviewContent;
    }

    public void ShowLeaderboard()
    {
        Leaderboard.instance.RetreiveLeaderboard();
    }
}
