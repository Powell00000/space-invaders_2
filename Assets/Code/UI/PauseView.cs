using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [Zenject.Inject] private GameplayController gameplayCtrl;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private GameObject pauseScreen;

    // Start is called before the first frame update
    private void Start()
    {
        pauseButton.onClick.AddListener(SwitchPause);
        gameplayCtrl.OnStateChanged += GameStateChanged;
    }

    private void GameStateChanged(EGameplayState currentState)
    {
        pauseScreen.SetActive(currentState == EGameplayState.Pause);
    }

    private void SwitchPause()
    {
        gameplayCtrl.SetPause(gameplayCtrl.CurrentGameplayState == EGameplayState.Playing);
    }

    private void OnDestroy()
    {
        gameplayCtrl.OnStateChanged -= GameStateChanged;
    }

    public void EndGame()
    {
        gameplayCtrl.EndGame();
    }
}
