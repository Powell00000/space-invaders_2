using Game.Gameplay;
using TMPro;
using UnityEngine;

namespace Assets.Code.UI
{
    internal class NewHighScoreView : MonoBehaviour
    {
        [Zenject.Inject] private PointsManager pointsManager;
        [Zenject.Inject] private LeaderboardController leaderboardCtrl;
        [SerializeField] private TMP_InputField inputField;

        public void SubmitScore()
        {
            Leaderboard.Entry entry = new Leaderboard.Entry();
            entry.PlayerName = inputField.text;
            entry.Points = pointsManager.CurrentPoints;

            leaderboardCtrl.AddEntry(entry);
            leaderboardCtrl.SaveToDisk();

            gameObject.SetActive(false);
        }
    }
}
