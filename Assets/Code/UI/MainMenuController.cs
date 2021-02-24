using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject leaderboards;

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowLeaderboards()
    {
        mainMenu.SetActive(false);
        leaderboards.SetActive(true);
    }

    public void ShowMenu()
    {
        mainMenu.SetActive(true);
        leaderboards.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
