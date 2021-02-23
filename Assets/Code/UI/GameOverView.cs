using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    internal class GameOverView : MonoBehaviour
    {
        [Zenject.Inject] private GameplayController gameplayController;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        private void RestartGame()
        {
            gameplayController.Restart();
        }
    }
}
