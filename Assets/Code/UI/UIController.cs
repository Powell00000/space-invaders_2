using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    //Controlls visibility of views
    public class UIController : MonoBehaviour, Zenject.IInitializable, System.IDisposable
    {
        [Zenject.Inject] private GameplayController gameplayCtrl = null;

        //TODO: REWORK to base class GUIView
        [SerializeField] private GameObject warmup = null;
        [SerializeField] private GameObject gameplay = null;
        [SerializeField] private GameObject gameOver = null;
        [SerializeField] private GameObject pause = null;

        public void Initialize()
        {
            gameplayCtrl.OnStateChanged += GameStateChanged;
            //gameplayCtrl.OnLevelRestarting += LevelRestarting;
        }

        private void Start()
        {
            GameStateChanged(gameplayCtrl.CurrentGameplayState);
        }

        private void GameStateChanged(EGameplayState state)
        {
            warmup.SetActive(gameplayCtrl.CurrentGameplayState == EGameplayState.Warmup);
            gameplay.SetActive(gameplayCtrl.CurrentGameplayState == EGameplayState.Playing);
            gameOver.SetActive(gameplayCtrl.CurrentGameplayState == EGameplayState.GameOver);
            pause.SetActive(gameplayCtrl.CurrentGameplayState == EGameplayState.Playing || gameplayCtrl.CurrentGameplayState == EGameplayState.Pause);
        }

        private void LevelRestarting()
        {
            warmup.SetActive(false);
            gameplay.SetActive(false);
            gameOver.SetActive(false);
        }

        public void Dispose()
        {
            if (gameplayCtrl != null)
            {
                gameplayCtrl.OnStateChanged -= GameStateChanged;
            }
        }
    }
}