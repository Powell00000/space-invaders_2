using Game.Gameplay;
using UnityEngine;

namespace Assets.Code.UI
{
    public class LifeView : MonoBehaviour
    {
        [Zenject.Inject] private PlayerController playerController;
        [SerializeField] protected TMPro.TextMeshProUGUI livesLabel;

        private void OnEnable()
        {
            playerController.OnPlayerHit += OnPlayerHit;
        }

        private void OnDisable()
        {
            playerController.OnPlayerHit -= OnPlayerHit;
        }

        private void OnPlayerHit()
        {
            livesLabel.text = $"x{playerController.LivesLeft}";
        }
    }
}