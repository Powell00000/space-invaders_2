using Assets.Code;
using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text number;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text points;

    public void SetData(int index, Leaderboard.Entry entry)
    {
        number.text = (index + 1).ToString();
        playerName.text = entry.PlayerName;
        points.text = entry.Points.ToString();
    }
}
