using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    internal class GameOverView : MonoBehaviour
    {
        [Zenject.Inject] private GameplayController gameplayController;
        [Zenject.Inject] private PointsManager pointsManager;
        [SerializeField] private Button restartButton;
        [SerializeField] private NewHighScoreView newHighScoreView;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            if (pointsManager.NewHighScore)
            {
                newHighScoreView.gameObject.SetActive(true);
            }
        }

        private void RestartGame()
        {
            gameplayController.Restart();
        }
    }
}
