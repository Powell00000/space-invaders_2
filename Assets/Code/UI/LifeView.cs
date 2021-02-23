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
            RefreshLabel();
        }

        private void OnDisable()
        {
            playerController.OnPlayerHit -= OnPlayerHit;
        }

        private void OnPlayerHit()
        {
            RefreshLabel();
        }

        private void RefreshLabel()
        {
            livesLabel.text = $"x{playerController.LivesLeft}";
        }
    }
}